using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public float SpawningStartTime = 0.0f;
    [SerializeField] public float TimeBetweenSpawns = 0.5f;
    [SerializeField] public EnemyType[] EnemySpawnPattern;
    private Queue<EnemyType> _enemies;
    private float _spawnTimer;
    private bool _isFinishedSpawning;

    private void Start()
    {
        //init variables
        _spawnTimer = 0.0f;
        _isFinishedSpawning = false;

        //init enemy queue
        _enemies = new Queue<EnemyType>();
        foreach (EnemyType enemy in EnemySpawnPattern)
        {
            _enemies.Enqueue(enemy);
        }
    }

    private void Update()
    {
        //check if can spawn
        if (!_isFinishedSpawning && DataManager.Instance.LevelDataObject.TimeSinceCrystalStart >= SpawningStartTime)
        {
            //check for spawn
            _spawnTimer -= Time.deltaTime;

            if (_spawnTimer <= 0.0f)
            {
                Spawn();
                _spawnTimer = TimeBetweenSpawns;
            }
        }        
    }

    private void Spawn()
    {
        if (_enemies.Count > 0)
        {
            switch (_enemies.Dequeue())
            {
                case EnemyType.Basic:
                    PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BasicEnemyPrefab.name, transform.position, transform.rotation);
                    break;

                case EnemyType.BasicAlt1:
                    PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BasicEnemyPrefab.name, transform.position, transform.rotation);
                    break;

                case EnemyType.BasicAlt2:
                    PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BasicEnemyPrefab.name, transform.position, transform.rotation);
                    break;

                case EnemyType.Brute:
                    PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BruteEnemyPrefab.name, transform.position, transform.rotation);
                    break;

                case EnemyType.BruteAlt1:
                    PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BruteEnemyPrefab.name, transform.position, transform.rotation);
                    break;

                case EnemyType.BruteAlt2:
                    PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BruteEnemyPrefab.name, transform.position, transform.rotation);
                    break;

                default:
                    //no spawn
                    break;
            }
        }
        else
        {
            _isFinishedSpawning = true;
        }
    }
}
