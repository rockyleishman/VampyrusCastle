using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreezingAura : EnemyStatusAura
{
    [SerializeField][Range(0.001f, 1.0f)] public float SpeedMultiplier = 0.5f;

    protected override void StatusApplication(DestructableObject destructableObject)
    {
        destructableObject.SpeedMultiplier *= SpeedMultiplier;
    }

    protected override void StatusRemoval(DestructableObject destructableObject)
    {
        destructableObject.SpeedMultiplier /= SpeedMultiplier;
    }
}
