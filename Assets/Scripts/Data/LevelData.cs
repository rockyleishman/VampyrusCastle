using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Basic,
    BasicAlt1,
    BasicAlt2,
    Brute,
    BruteAlt1,
    BruteAlt2,
}

[CreateAssetMenu(fileName = "LevelDataObject", menuName = "Data/LevelDataObject", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Enemy Prefabs")]
    [SerializeField] public EnemyAIController BasicEnemyPrefab;
    [SerializeField] public EnemyAIController BasicEnemyAlt1Prefab;
    [SerializeField] public EnemyAIController BasicEnemyAlt2Prefab;
    [SerializeField] public EnemyAIController BruteEnemyPrefab;
    [SerializeField] public EnemyAIController BruteEnemyAlt1Prefab;
    [SerializeField] public EnemyAIController BruteEnemyAlt2Prefab;

    [Header("UI Settings")]
    [SerializeField] public int VisualMaxCandy = 100;

    //displayed by custom editor
    internal Vector2[] SpawnLocations;
    internal int WaveCount = 10;

    //hiden from inspector
    internal PathPoint[] PathPoints;
    internal CrystalController Crystal;
    internal float TimeSinceCrystalStart;
    internal float CrystalChargePercent;
}
