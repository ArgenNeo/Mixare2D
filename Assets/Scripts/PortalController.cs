using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public bool canWarp;
    int enemyTagsCount;

    private void Start()
    {
        canWarp = false;
    }

    void Update()
    {
        enemyTagsCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyTagsCount == 0)
        {
            canWarp = true;
        }
    }
}
