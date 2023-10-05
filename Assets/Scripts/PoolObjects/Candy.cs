using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : PoolObject
{
    [SerializeField] public GameEvent CandyCollectionEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other?.GetComponent<PlayerController>() != null)
        {
            //trigger candy collection event
            CandyCollectionEvent.TriggerEvent(transform.position);

            //despawn
            OnDespawn();
        }
    }
}
