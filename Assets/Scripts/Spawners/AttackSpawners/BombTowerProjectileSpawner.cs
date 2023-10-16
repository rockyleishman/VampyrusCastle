using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTowerProjectileSpawner : AttackSpawner
{
    [SerializeField] public FriendlyExplosiveProjectile BombTowerProjectile;
    [SerializeField] public float DirectDamage = 10.0f;
    [SerializeField] public float AreaDamage = 5.0f;
    [SerializeField] public float Radius = 1.5f;
    [SerializeField] public float ProjectileSpeed = 10.0f;
    [SerializeField] public float KnockbackDistance = 1.0f;
    [SerializeField] public float KnockbackVelocity = 20.0f;

    public override void TriggerAttack(Vector3 attackOrigin, Vector3 attackDirection, DestructableObject attackTarget)
    {
        //spawn projectile
        FriendlyExplosiveProjectile projectile = (FriendlyExplosiveProjectile)PoolManager.Instance.Spawn(BombTowerProjectile.name, attackOrigin, Quaternion.LookRotation(Vector3.forward, -attackDirection));
        projectile.Init(DirectDamage, AreaDamage, Radius, Vector3.Distance(attackOrigin, attackTarget.transform.position), ProjectileSpeed, KnockbackDistance, KnockbackVelocity);
    }
}
