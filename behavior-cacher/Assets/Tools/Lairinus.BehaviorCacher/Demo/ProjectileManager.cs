using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ProjectileManager : MonoBehaviour
{
    private List<Projectile> _projectiles = null;

    // Update is called once per frame
    private void Update()
    {
        _projectiles = BehaviourCacheDemo.projectileCache.GetCachedValues();
        _projectiles.Where(x => x.gameObject.activeInHierarchy).ToList().ForEach(x => x.UpdateMovement());
    }
}