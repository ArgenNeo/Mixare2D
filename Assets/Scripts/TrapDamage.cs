using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int spikeDamage = 20;
    public Vector2 knockbackDirection = new Vector2(0, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool spikeHit = damageable.Hit(spikeDamage, knockbackDirection);
        }
    }
}
