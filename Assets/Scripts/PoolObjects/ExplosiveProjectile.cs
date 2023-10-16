using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : PoolObject
{
    [SerializeField] public Effect AttackEffect;
    [SerializeField] public Effect ExplosionEffect;
    protected float _directDamage;
    protected float _areaDamage;
    protected float _radius;
    protected float _distance;
    protected Vector3 _origin;
    protected Vector3 _velocity;
    protected float _knockbackDistance;
    protected float _knockbackTime;

    public virtual void Init(float directDamage, float areaDamage, float radius, float distance, float speed, float knockbackDistance, float knockbackVelocity)
    {
        //init
        _directDamage = directDamage;
        _areaDamage = areaDamage;
        _radius = radius;
        _distance = distance;
        _origin = transform.position;
        _velocity = Vector3.down * speed;
        _knockbackDistance = knockbackDistance;
        _knockbackTime = knockbackDistance / knockbackVelocity;

        //spawn effect
        Effect attackEffect = (Effect)PoolManager.Instance.Spawn(AttackEffect.name, transform.position, transform.rotation);
        attackEffect.Init();
    }

    protected void Update()
    {
        //move
        transform.Translate(_velocity * Time.deltaTime);

        //check if at target location
        if (Vector3.Distance(_origin, transform.position) >= _distance)
        {
            //explode
            Explode();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_directDamage > 0.0f || _areaDamage > 0.0f)
        {
            DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

            if (destructableObject != null)
            {
                //deal direct damage (area damage applies knockback)
                destructableObject.DamageHP(_directDamage);
                destructableObject.ActivateIFrames();

                //deal area damage
                Explode();
            }
        }
    }

    protected virtual void Explode()
    {
        //spawn explosion effect
        Effect hitEffect = (Effect)PoolManager.Instance.Spawn(ExplosionEffect.name, transform.position, transform.rotation);
        hitEffect.Init();

        //detect and affect destructable objects
        RaycastHit2D[] ExplosionHits = Physics2D.CircleCastAll(transform.position, _radius, Vector2.down, 0.0f);
        foreach (RaycastHit2D hit in ExplosionHits)
        {
            DestructableObject destructableObject = hit.collider?.GetComponent<DestructableObject>();

            if (destructableObject != null)
            {
                ObjectHit(destructableObject);
            }
        }

        //despawn
        OnDespawn();
    }

    protected void ObjectHit(DestructableObject destructableObject)
    {
        //damage destructable object
        destructableObject.DamageHP(_areaDamage);
        destructableObject.Knockback((destructableObject.transform.position - transform.position).normalized * _knockbackDistance, _knockbackTime);
        destructableObject.ActivateIFrames();
    }
}
