using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiveProjectile : ExplosiveProjectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (_damage > 0.0f)
        {
            DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

            if (destructableObject != null && !(destructableObject is EnemyAIController))
            {
                Explode();
            }
        }
    }

    protected override void Explode()
    {
        //spawn explosion effect
        Effect hitEffect = (Effect)PoolManager.Instance.Spawn(ExplosionEffect.name, transform.position, transform.rotation);
        hitEffect.Init();

        //detect and affect destructable objects
        RaycastHit2D[] ExplosionHits = Physics2D.CircleCastAll(transform.position, _radius, Vector2.down, 0.0f);
        foreach (RaycastHit2D hit in ExplosionHits)
        {
            DestructableObject destructableObject = hit.collider?.GetComponent<DestructableObject>();

            if (destructableObject != null && !(destructableObject is EnemyAIController))
            {
                ObjectHit(destructableObject);
            }
        }

        //despawn
        OnDespawn();
    }
}
