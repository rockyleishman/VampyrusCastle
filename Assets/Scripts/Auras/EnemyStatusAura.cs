using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyStatusAura : Aura
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

        if (destructableObject != null && (destructableObject is EnemyAIController))
        {
            ApplyStatus(destructableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

        if (destructableObject != null && (destructableObject is EnemyAIController))
        {
            RemoveStatus(destructableObject);
        }
    }
}
