using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackSpawner : AttackSpawner
{
    [SerializeField] public PlayerMeleeAttack MeleeAttack;
    [SerializeField] public float Damage = 5.0f;
    [SerializeField] public float Range = 1.5f;
    [SerializeField] public float KnockbackDistance = 2.0f;
    [SerializeField] public float KnockbackVelocity = 20.0f;

    public override void TriggerAttack(Vector3 attackOrigin, Vector3 attackDirection, DestructableObject attackTarget)
    {
        //spawn attack
        PlayerMeleeAttack meleeAttack = (PlayerMeleeAttack)PoolManager.Instance.Spawn(MeleeAttack.name, attackOrigin + attackDirection * Range, Quaternion.LookRotation(Vector3.forward, -attackDirection));
        meleeAttack.Init(Damage, KnockbackDistance, KnockbackVelocity);
    }
}
