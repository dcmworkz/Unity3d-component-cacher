using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Lairinus.Performance;

public class Projectile : MonoBehaviour
{
    public Vector3 direction = new Vector3();
    private GameObject _cachedGameObject = null;
    private Transform _cachedTransform = null;
    private bool _isInitialized = false;

    public void Create(Transform launcher)
    {
        Initialize();
        _cachedTransform.position = launcher.position;
        _cachedTransform.rotation = launcher.rotation;
        _cachedGameObject.SetActive(true);
    }

    public void DoSomething()
    {
        // something done
    }

    public void OnTriggerEnter(Collider other)
    {
        DemoLevelState.totalCollisions++;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        if (DemoLevelState.useDemoCache)
        {
            Projectile foundProjectile = BehaviourCacheDemo.projectileCache.Get(other);
            DemoLevelState.componentsReadFromCache++;
            if (foundProjectile != null)
                foundProjectile.DoSomething();
        }
        else
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
                projectile.DoSomething();
        }
        sw.Stop();
        DemoLevelState.totalOnTriggerEnters++;
        DemoLevelState.totalCalculationTime += sw.ElapsedTicks;
    }

    public void UpdateMovement()
    {
        _cachedTransform.Translate(direction * 10 * Time.deltaTime, Space.World);
    }

    private void Initialize()
    {
        if (_isInitialized)
            return;

        _isInitialized = true;
        _cachedGameObject = gameObject;
        _cachedTransform = transform;
        BehaviourCacheDemo.projectileCache.Add(this);
    }

    private void Start()
    {
        Initialize();
    }
}