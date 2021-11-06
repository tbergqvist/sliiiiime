using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float damage;

    public float dealDamageCooldown;

    float dealDamageTimer;

    private void Update()
    {
        dealDamageTimer -= Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Slime slime) && dealDamageTimer <= 0)
        {
            slime.TakeDamage(damage);
            dealDamageTimer = dealDamageCooldown;
        }
    }
}
