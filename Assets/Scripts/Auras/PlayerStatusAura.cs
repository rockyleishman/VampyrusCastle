using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStatusAura : Aura
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

        if (destructableObject != null && (destructableObject is PlayerController))
        {
            ApplyStatus(destructableObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DestructableObject destructableObject = other?.GetComponent<DestructableObject>();

        if (destructableObject != null && (destructableObject is PlayerController))
        {
            RemoveStatus(destructableObject);
        }
    }
}
