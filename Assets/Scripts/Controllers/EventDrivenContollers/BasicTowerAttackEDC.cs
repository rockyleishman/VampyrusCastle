using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerAttackEDC : AttackEDC
{
    [SerializeField] public FriendlyProjectile BasicTowerProjectile;
    [SerializeField] public float Damage = 5.0f;
    [SerializeField][Range(0, 20)] public int Pierce = 0;
    [SerializeField] public float ProjectileRange = 6.5f;
    [SerializeField] public float ProjectileSpeed = 20.0f;
    [SerializeField] public float KnockbackDistance = 1.0f;
    [SerializeField] public float KnockbackVelocity = 20.0f;

    public override void TriggerAttack(Vector3 attackOrigin, Vector3 attackDirection, DestructableObject attackTarget)
    {
        //spawn projectile
        FriendlyProjectile projectile = (FriendlyProjectile)PoolManager.Instance.Spawn(BasicTowerProjectile.name, attackOrigin, Quaternion.LookRotation(Vector3.forward, -attackDirection));
        projectile.Init(Damage, Pierce, ProjectileRange, ProjectileSpeed, KnockbackDistance, KnockbackVelocity);
    }
}
