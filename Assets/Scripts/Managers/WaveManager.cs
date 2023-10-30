using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
    internal bool IsSpawningStarted;
    internal bool IsSpawningCompleted;
    internal int CurrentWave;
    private int _locationsCompletedForCurrentWave;
    private float _timeSinceSpawningCompleted;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        IsSpawningStarted = false;
        IsSpawningCompleted = false;
        CurrentWave = 0;
        _locationsCompletedForCurrentWave = 0;
        _timeSinceSpawningCompleted = 0;
        DataManager.Instance.LevelDataObject.TimeToNextWave = 0.0f;
    }

    private void Update()
    {
        if (IsSpawningStarted && !IsSpawningCompleted)
        {
            //update time
            DataManager.Instance.LevelDataObject.TimeToNextWave -= Time.deltaTime;
            HUDManager.Instance.UpdateTime();

            if (DataManager.Instance.LevelDataObject.TimeToNextWave <= 0.0f)
            {
                SpawnWave(CurrentWave);
            }
        }
        else if (IsSpawningCompleted)
        {
            _timeSinceSpawningCompleted += Time.deltaTime;
        }

        if (_timeSinceSpawningCompleted > LevelData.FinalWaveMinTime && DataManager.Instance.LevelDataObject.EnemiesRemaining == 0)
        {
            DataManager.Instance.EventDataObject.LoadNextLevel.TriggerEvent(transform.position);
        }
    }

    private void SpawnWave(int waveIndex)
    {
        //reset location completion
        _locationsCompletedForCurrentWave = 0;

        //reset wave time limit
        DataManager.Instance.LevelDataObject.TimeToNextWave = DataManager.Instance.LevelDataObject.WaveTimeLimits[waveIndex];

        for (int i = 0; i < DataManager.Instance.LevelDataObject.SpawnLocations.Length; i++)
        {
            StartCoroutine(SpawnWaveAtLocation(waveIndex, i));
        }

        //increment current wave
        CurrentWave++;
        HUDManager.Instance.UpdateWave();

        //stop future spawns of last wave
        if (CurrentWave >= DataManager.Instance.LevelDataObject.WaveCount)
        {
            IsSpawningCompleted = true;
            HUDManager.Instance.UpdateTime();
        }
    }

    private IEnumerator SpawnWaveAtLocation(int waveIndex, int locationIndex)
    {
        //create spawning stack
        Queue<EnemyType> spawnStack = new Queue<EnemyType>();
        for (int i = 0; i < DataManager.Instance.LevelDataObject.EnemyCounts[waveIndex, locationIndex]; i++)
        {
            spawnStack.Enqueue(DataManager.Instance.LevelDataObject.Enemies[waveIndex, locationIndex, i]);
        }

        //spawn from stack with delays
        while (spawnStack.Count > 0)
        {
            switch (spawnStack.Dequeue())
            {
                case EnemyType.Basic:
                    DataManager.Instance.EventDataObject.SpawnEnemyBasic.TriggerEvent(DataManager.Instance.LevelDataObject.SpawnLocations[locationIndex]);
                    break;

                case EnemyType.BasicAlt1:
                    DataManager.Instance.EventDataObject.SpawnEnemyBasicAlt1.TriggerEvent(DataManager.Instance.LevelDataObject.SpawnLocations[locationIndex]);
                    break;

                case EnemyType.BasicAlt2:
                    DataManager.Instance.EventDataObject.SpawnEnemyBasicAlt2.TriggerEvent(DataManager.Instance.LevelDataObject.SpawnLocations[locationIndex]);
                    break;

                case EnemyType.Brute:
                    DataManager.Instance.EventDataObject.SpawnEnemyBrute.TriggerEvent(DataManager.Instance.LevelDataObject.SpawnLocations[locationIndex]);
                    break;

                case EnemyType.BruteAlt1:
                    DataManager.Instance.EventDataObject.SpawnEnemyBruteAlt1.TriggerEvent(DataManager.Instance.LevelDataObject.SpawnLocations[locationIndex]);
                    break;

                case EnemyType.BruteAlt2:
                    DataManager.Instance.EventDataObject.SpawnEnemyBruteAlt2.TriggerEvent(DataManager.Instance.LevelDataObject.SpawnLocations[locationIndex]);
                    break;

                default:
                    //no spawn
                    break;
            }

            //wait for spawn spacing time delay
            yield return new WaitForSeconds(DataManager.Instance.LevelDataObject.SpacingTimes[waveIndex, locationIndex]);
        }

        //count location as completed
        _locationsCompletedForCurrentWave++;
    }

    public void StartSpawningWaves()
    {
        IsSpawningStarted = true;
    }

    public void AdvanceToNextWave()
    {
        if (_locationsCompletedForCurrentWave >= DataManager.Instance.LevelDataObject.SpawnLocations.Length)
        {
            DataManager.Instance.LevelDataObject.TimeToNextWave = 0.0f;
        }
    }
}
