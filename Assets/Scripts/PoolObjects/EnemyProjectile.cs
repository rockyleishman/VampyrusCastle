using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (_damage > 0.0f)
        {
            DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

            if (destructableObject != null && !(destructableObject is EnemyAIController))
            {
                ObjectHit(destructableObject);
            }
        }
    }
}
