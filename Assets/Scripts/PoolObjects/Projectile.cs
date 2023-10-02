using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolObject
{
    [SerializeField] public Effect AttackEffect;
    [SerializeField] public Effect HitEffect;
    protected float _damage;
    protected int _chargesRemaining;
    protected float _range;
    protected Vector3 _origin;
    protected Vector3 _velocity;
    protected Vector3 _knockback;
    protected float _knockbackTime;

    public virtual void Init(float damage, int pierce, float range, float speed, float knockbackDistance, float knockbackVelocity)
    {
        //init
        _damage = damage;
        _chargesRemaining = pierce + 1;
        _range = range;
        _origin = transform.position;
        _velocity = Vector3.down * speed;
        _knockback = -transform.up * knockbackDistance;
        _knockbackTime = knockbackDistance / knockbackVelocity;

        //spawn effect
        Effect attackEffect = (Effect)PoolManager.Instance.Spawn(AttackEffect.name, transform.position, transform.rotation);
        attackEffect.Init();
    }

    protected void Update()
    {
        //move
        transform.Translate(_velocity * Time.deltaTime);

        //check if expired
        if (Vector3.Distance(_origin, transform.position) >= _range || _chargesRemaining <= 0)
        {
            //despawn
            OnDespawn();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (_damage > 0.0f)
        {
            DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

            if (destructableObject != null)
            {
                ObjectHit(destructableObject);
            }
        }
    }

    protected void ObjectHit(DestructableObject destructableObject)
    {
        //damage destructable object
        destructableObject.DamageHP(_damage);
        destructableObject.Knockback(_knockback, _knockbackTime);
        destructableObject.ActivateIFrames();

        //decrement charge remaining
        _chargesRemaining--;

        //spawn hit effect
        Effect hitEffect = (Effect)PoolManager.Instance.Spawn(HitEffect.name, transform.position, transform.rotation);
        hitEffect.Init();
    }
}
