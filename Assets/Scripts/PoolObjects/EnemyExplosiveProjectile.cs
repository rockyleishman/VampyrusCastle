using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiveProjectile : ExplosiveProjectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (_directDamage > 0.0f || _areaDamage > 0.0f)
        {
            DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

            if (destructableObject != null && !(destructableObject is EnemyAIController))
            {
                //deal direct damage (area damage applies knockback)
                destructableObject.DamageHP(_directDamage);
                destructableObject.Knockback((destructableObject.transform.position - transform.position).normalized * _knockbackDistance, _knockbackTime);
                destructableObject.ActivateIFrames();

                //deal area damage
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
