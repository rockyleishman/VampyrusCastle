using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : PoolObject
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other?.GetComponent<PlayerController>() != null)
        {
            //add to candy count
            DataManager.Instance.PlayerDataObject.Candy++;

            //despawn
            OnDespawn();
        }
    }
}
