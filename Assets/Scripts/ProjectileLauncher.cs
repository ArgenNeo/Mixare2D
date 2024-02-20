using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefabs;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefabs, launchPoint.position, projectilePrefabs.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;

        // Flip the projectile's facing direction and movement based on the direction the character is facing at time of launch
        projectile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x > 0 ? 1 : -1,
            origScale.y,
            origScale.z
            );
    }
    
}
