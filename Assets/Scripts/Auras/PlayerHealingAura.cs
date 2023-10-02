using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealingAura : PlayerStatusAura
{
    [SerializeField] public float HealingPerSecond = 1.0f;

    protected override void StatusApplication(DestructableObject destructableObject)
    {
        destructableObject.HealingPerSecond += HealingPerSecond;
    }

    protected override void StatusRemoval(DestructableObject destructableObject)
    {
        destructableObject.HealingPerSecond -= HealingPerSecond;
    }
}
