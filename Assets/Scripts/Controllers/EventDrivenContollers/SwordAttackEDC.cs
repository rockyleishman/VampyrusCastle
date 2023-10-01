using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackEDC : AttackEDC
{
    [SerializeField] public PlayerMeleeAttack MeleeAttack;
    [SerializeField] public float Damage = 5.0f;
    [SerializeField] public float Range = 1.5f;
    [SerializeField] public float KnockbackDistance = 2.0f;
    [SerializeField] public float KnockbackVelocity = 20.0f;

    public override void TriggerAttack(Vector3 attackOrigin, Quaternion attackRotation, DestructableObject attackTarget)
    {
        //spawn attack
        PlayerMeleeAttack meleeAttack = (PlayerMeleeAttack)PoolManager.Instance.Spawn(MeleeAttack.name, attackOrigin + (attackRotation * Vector3.down * Range), attackRotation);
        meleeAttack.Init(Damage, KnockbackDistance, KnockbackVelocity);
    }
}
