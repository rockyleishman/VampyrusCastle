using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalWave : MonoBehaviour
{
    [SerializeField] public Vector2 SpawnLocation;
    [SerializeField] [Range(0.05f, 5.0f)] public float SpacingTime = 0.5f;
    [SerializeField] public EnemyType[] Enemies;
}
