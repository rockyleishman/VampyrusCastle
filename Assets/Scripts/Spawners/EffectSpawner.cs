using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] public Effect PlayerHitEffect;
    [SerializeField] public Effect PlayerDeathEffect;
    [SerializeField] public Effect CrystalHitEffect;
    [SerializeField] public Effect CrystalDeathEffect;
    [SerializeField] public Effect EnemyHitEffect;
    [SerializeField] public Effect EnemyDeathEffect;

    public void SpawnPlayerHitEffect(Vector3 location)
    {
        Effect effect = (Effect)PoolManager.Instance.Spawn(PlayerHitEffect.name, location, Quaternion.identity);
        effect.Init();
    }

    public void SpawnPlayerDeathEffect(Vector3 location)
    {
        Effect effect = (Effect)PoolManager.Instance.Spawn(PlayerDeathEffect.name, location, Quaternion.identity);
        effect.Init();
    }

    public void SpawnCrystalHitEffect(Vector3 location)
    {
        Effect effect = (Effect)PoolManager.Instance.Spawn(CrystalHitEffect.name, location, Quaternion.identity);
        effect.Init();
    }

    public void SpawnCrystalDeathEffect(Vector3 location)
    {
        Effect effect = (Effect)PoolManager.Instance.Spawn(CrystalDeathEffect.name, location, Quaternion.identity);
        effect.Init();
    }

    public void SpawnEnemyHitEffect(Vector3 location)
    {
        Effect effect = (Effect)PoolManager.Instance.Spawn(EnemyHitEffect.name, location, Quaternion.identity);
        effect.Init();
    }

    public void SpawnEnemyDeathEffect(Vector3 location)
    {
        Effect effect = (Effect)PoolManager.Instance.Spawn(EnemyDeathEffect.name, location, Quaternion.identity);
        effect.Init();
    }
}
