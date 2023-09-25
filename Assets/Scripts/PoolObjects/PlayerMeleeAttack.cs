using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : PoolObject
{
    [SerializeField] public Effect AttackEffect;
    [SerializeField] public float Persistence = 0.1f;
    private float _lifeTimeRemaining;
    private float _damage;
    private Vector3 _knockback;
    private float _knockbackTime;

    public void Init(float damage, float knockbackDistance, float knockbackVelocity)
    {
        //init
        _lifeTimeRemaining = Persistence;
        _damage = damage;
        _knockback = -transform.up * knockbackDistance;
        _knockbackTime = knockbackDistance / knockbackVelocity;

        //spawn effect
        Effect attackEffect = (Effect)PoolManager.Instance.Spawn(AttackEffect.name, transform.position, transform.rotation);
        attackEffect.Init();
    }

    private void Update()
    {
        //update life time remaining
        _lifeTimeRemaining -= Time.deltaTime;

        //check existence
        if (_lifeTimeRemaining <= 0.0f)
        {
            //despawn
            OnDespawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_damage > 0.0f)
        {
            DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

            if (destructableObject != null && !(destructableObject is PlayerController))
            {
                //damage destructable object
                destructableObject.DamageHP(_damage);
                destructableObject.Knockback(_knockback, _knockbackTime);
                destructableObject.ActivateIFrames();
            }
        }
    }
}
