using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    [SerializeField] public float MaxDistanceFromSource = 1.0f;
    [SerializeField] public Candy[] CandyPrefabs; 

    public void SpawnCandy(Vector3 location)
    {
        //choose random candy to spawn
        Candy candyPrefab = CandyPrefabs[Random.Range(0, CandyPrefabs.Length)];

        //spawn candy
        PoolManager.Instance.Spawn(candyPrefab.name, location + ((Vector3)Random.insideUnitCircle * MaxDistanceFromSource), Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f)));
    }
}
