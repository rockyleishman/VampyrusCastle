using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableObject : PoolObject
{
    [Header("Health")]
    [SerializeField] public float MaxHP = 100;
    protected float _currentHP;

    protected virtual void Start()
    {
        InitHP();
    }

    protected void InitHP()
    {
        _currentHP = MaxHP;
    }

    public void DamageHP(float hp)
    {
        _currentHP -= hp;

        if (_currentHP < 0.0f)
        {
            _currentHP = 0.0f;
            Destroy();
        }
    }

    public void HealHP(float hp)
    {
        _currentHP += hp;

        if (_currentHP > MaxHP)
        {
            _currentHP = MaxHP;
        }
    }

    protected abstract void Destroy();
}
