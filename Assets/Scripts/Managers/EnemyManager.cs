using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    private void Update()
    {
        //update enemies remaining
        DataManager.Instance.LevelDataObject.EnemiesRemaining = GetComponentsInChildren<EnemyAIController>().GetLength(0);
        HUDManager.Instance.UpdateEnemies();
    }

    public void SpawnBasic(Vector3 position)
    {
        EnemyAIController enemy = (EnemyAIController)PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BasicEnemyPrefab.name, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }

    public void SpawnBasicAlt1(Vector3 position)
    {
        EnemyAIController enemy = (EnemyAIController)PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BasicEnemyAlt1Prefab.name, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }

    public void SpawnBasicAlt2(Vector3 position)
    {
        EnemyAIController enemy = (EnemyAIController)PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BasicEnemyAlt2Prefab.name, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }

    public void SpawnBrute(Vector3 position)
    {
        EnemyAIController enemy = (EnemyAIController)PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BruteEnemyPrefab.name, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }

    public void SpawnBruteAlt1(Vector3 position)
    {
        EnemyAIController enemy = (EnemyAIController)PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BruteEnemyAlt1Prefab.name, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }

    public void SpawnBruteAlt2(Vector3 position)
    {
        EnemyAIController enemy = (EnemyAIController)PoolManager.Instance.Spawn(DataManager.Instance.LevelDataObject.BruteEnemyAlt2Prefab.name, position, Quaternion.identity);
        enemy.transform.SetParent(transform);
    }
}
