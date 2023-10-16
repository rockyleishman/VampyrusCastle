using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    Ranged,
    Brute
}

[CreateAssetMenu(fileName = "LevelDataObject", menuName = "Data/LevelDataObject", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Enemy Prefabs")]
    [SerializeField] public EnemyAIController BasicEnemyPrefab;
    [SerializeField] public EnemyAIController RangedEnemyPrefab;
    [SerializeField] public EnemyAIController BruteEnemyPrefab;

    [Header("UI Settings")]
    [SerializeField] public int VisualMaxCandy = 100;

    internal PathPoint[] PathPoints;
    internal float TimeSinceCrystalStart;
    internal float CrystalChargePercent;
}
