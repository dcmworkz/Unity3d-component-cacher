using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public float launchSpeed = 0;
    public List<Projectile> projectiles = new List<Projectile>();
    private Transform _cachedTransform = null;

    private IEnumerator Start()
    {
        _cachedTransform = transform;
        int currentIndex = 0;
        while (true)
        {
            yield return new WaitForSeconds(launchSpeed);
            projectiles[currentIndex].Create(_cachedTransform);

            if (currentIndex == projectiles.Count - 1)
                currentIndex = 0;
            else currentIndex++;
        }
    }
}