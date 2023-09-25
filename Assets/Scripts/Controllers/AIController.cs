using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : DestructableObject
{
    [Header("Enemy Attributes")]
    [SerializeField] public float EnemySpeed = 4.0f;
    [SerializeField] public float CollisionDamage = 5.0f;
    [SerializeField] public float CollisionCooldown = 1f;
    [SerializeField] public float CollisionKnockbackDistance = 2.0f;
    [SerializeField] public float CollisionKnockbackVelocity = 20.0f;
    private float _collisionCooldownTimer;
    private bool _isCollisionReady;

    protected override void Start()
    {
        base.Start();

        _collisionCooldownTimer = 0.0f;
        _isCollisionReady = true;
    }

    protected override void Update()
    {
        base.Update();

        //update collision readiness
        _collisionCooldownTimer -= Time.deltaTime;

        if (_collisionCooldownTimer <= 0.0f)
        {
            _isCollisionReady = true;
        }
        else
        {
            _isCollisionReady = false;
        }
    }

    //collision damage
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CollisionDamage > 0.0f && _isCollisionReady)
        {
            PlayerController player = collision.collider?.GetComponent<PlayerController>();

            if (player != null)
            {
                player.DamageHP(CollisionDamage);
                player.Knockback((player.transform.position - transform.position).normalized * CollisionKnockbackDistance, CollisionKnockbackDistance / CollisionKnockbackVelocity);
                player.ActivateIFrames();
                _collisionCooldownTimer = CollisionCooldown;
            }
        }
    }

    protected override void DestroyObject()
    {
        OnDespawn();
    }
}
