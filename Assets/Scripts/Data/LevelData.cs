using System.Collections.Generic;
using UnityEngine;

//for serializing 2D arrays
[System.Serializable]
public struct Serializable2DArrayPackage<TElement>
{
    public int Index0;
    public int Index1;
    public TElement Element;

    public Serializable2DArrayPackage(int index0, int index1, TElement element)
    {
        Index0 = index0;
        Index1 = index1;
        Element = element;
    }
}

//for serializing 3D arrays
[System.Serializable]
public struct Serializable3DArrayPackage<TElement>
{
    public int Index0;
    public int Index1;
    public int Index2;
    public TElement Element;

    public Serializable3DArrayPackage(int index0, int index1, int index2, TElement element)
    {
        Index0 = index0;
        Index1 = index1;
        Index2 = index2;
        Element = element;
    }
}

//enemy types
public enum EnemyType
{
    Basic,
    BasicAlt1,
    BasicAlt2,
    Brute,
    BruteAlt1,
    BruteAlt2,
}

//level data class
[CreateAssetMenu(fileName = "LevelDataObject", menuName = "Data/LevelDataObject", order = 0)]
public class LevelData : ScriptableObject, ISerializationCallbackReceiver
{
    //UI settings
    [Header("UI Settings")]
    [SerializeField] public int VisualMaxCandy = 50;
    [SerializeField] public int VisualMaxEnemies = 25;
    [SerializeField] public int WaveTimeWarning = 5;

    //crystal settings
    [Header("Crystal Settings")]
    [SerializeField] public float MaxCrystalHP = 100;
    internal float CrystalHP;
    internal CrystalController Crystal;

    //enemy prefabs
    [Header("Enemy Prefabs")]
    [SerializeField] public EnemyAIController BasicEnemyPrefab;
    [SerializeField] public EnemyAIController BasicEnemyAlt1Prefab;
    [SerializeField] public EnemyAIController BasicEnemyAlt2Prefab;
    [SerializeField] public EnemyAIController BruteEnemyPrefab;
    [SerializeField] public EnemyAIController BruteEnemyAlt1Prefab;
    [SerializeField] public EnemyAIController BruteEnemyAlt2Prefab;

    //wave settings
    public const int MaxWaveCount = 128;
    public const int MaxSpawnLocations = 4;
    public const int MaxEnemiesPerSpawnLocation = 256;
    [Header("Wave Settings")]
    [SerializeField] [Range(1, MaxWaveCount)] public int WaveCount = 10;
    [SerializeField] public Vector2[] SpawnLocations;

    //used by custom editor
    [SerializeField, HideInInspector] public bool DoNotDeleteThisBool; //somehow changing this value to anything fixes serialization issues when restarting the unity editor
    [SerializeField, HideInInspector] public float[] WaveTimeLimits; //[wave]
    [HideInInspector] public int[,] EnemyCounts; //per spawnLocation //[wave, spawnLocation]
    [SerializeField, HideInInspector] internal List<Serializable2DArrayPackage<int>> SerializableEnemyCounts; //for multidimentional array serialization
    [HideInInspector] public float[,] SpacingTimes; //[wave, spawnLocation]
    [SerializeField, HideInInspector] internal List<Serializable2DArrayPackage<float>> SerializableSpacingTimes; //for multidimentional array serialization
    [HideInInspector] public EnemyType[,,] Enemies; //[wave, spawnLocation, index]
    [SerializeField, HideInInspector] internal List<Serializable3DArrayPackage<EnemyType>> SerializableEnemies; //for multidimentional array serialization

    //hiden from inspector
    internal PathPoint[] PathPoints;
    internal float TimeSinceCrystalStart;
    internal float TimeToNextWave;
    internal int EnemiesRemaining;

    private void InitializeArrays()
    {
        if (SpawnLocations == null)
        {
            SpawnLocations = new Vector2[0];
        }
        if (WaveTimeLimits == null)
        {
            WaveTimeLimits = new float[MaxWaveCount];
        }
        if (EnemyCounts == null)
        {
            EnemyCounts = new int[MaxWaveCount, MaxSpawnLocations];
        }
        if (SpacingTimes == null)
        {
            SpacingTimes = new float[MaxWaveCount, MaxSpawnLocations];
        }
        if (Enemies == null)
        {
            Enemies = new EnemyType[MaxWaveCount, MaxSpawnLocations, MaxEnemiesPerSpawnLocation];
        }
    }

    //multidimentional array serialization
    public void OnBeforeSerialize()
    {
        InitializeArrays();

        //serialize enemy counts
        SerializableEnemyCounts = new List<Serializable2DArrayPackage<int>>();
        for (int i = 0; i < EnemyCounts.GetLength(0); i++)
        {
            for (int j = 0; j < EnemyCounts.GetLength(1); j++)
            {
                SerializableEnemyCounts.Add(new Serializable2DArrayPackage<int>(i, j, EnemyCounts[i, j]));
            }
        }

        //serialize spacing time        
        SerializableSpacingTimes = new List<Serializable2DArrayPackage<float>>();
        for (int i = 0; i < SpacingTimes.GetLength(0); i++)
        {
            for (int j = 0; j < SpacingTimes.GetLength(1); j++)
            {
                SerializableSpacingTimes.Add(new Serializable2DArrayPackage<float>(i, j, SpacingTimes[i, j]));
            }
        }

        //serialize enemies        
        SerializableEnemies = new List<Serializable3DArrayPackage<EnemyType>>();
        for (int i = 0; i < Enemies.GetLength(0); i++)
        {
            for (int j = 0; j < Enemies.GetLength(1); j++)
            {
                for (int k = 0; k < Enemies.GetLength(2); k++)
                    SerializableEnemies.Add(new Serializable3DArrayPackage<EnemyType>(i, j, k, Enemies[i, j, k]));
            }
        }
    }

    //multidimentional array deserialization
    public void OnAfterDeserialize()
    {
        //deserialize enemy counts
        EnemyCounts = new int[MaxWaveCount, MaxSpawnLocations];
        foreach (Serializable2DArrayPackage<int> package in SerializableEnemyCounts)
        {
            EnemyCounts[package.Index0, package.Index1] = package.Element;
        }

        //deserialize spacing time
        SpacingTimes = new float[MaxWaveCount, MaxSpawnLocations];
        foreach (Serializable2DArrayPackage<float> package in SerializableSpacingTimes)
        {
            SpacingTimes[package.Index0, package.Index1] = package.Element;
        }

        //deserialize enemies
        Enemies = new EnemyType[MaxWaveCount, MaxSpawnLocations, MaxEnemiesPerSpawnLocation];
        foreach (Serializable3DArrayPackage<EnemyType> package in SerializableEnemies)
        {
            Enemies[package.Index0, package.Index1, package.Index2] = package.Element;
        }
    }    
}
