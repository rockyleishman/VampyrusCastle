using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableObject : PoolObject
{
    [Header("Health")]
    [SerializeField] public float MaxHP = 100;
    [SerializeField] protected float _currentHP;
    [SerializeField] public float IFramesTime = 0.5f;
    private float _iFrameTimeLeft;

    protected virtual void Start()
    {
        InitHP();
    }

    protected virtual void Update()
    {
        //decrease i frame time left
        _iFrameTimeLeft -= Time.deltaTime;
    }

    protected void InitHP()
    {
        _currentHP = MaxHP;
        _iFrameTimeLeft = 0.0f;
    }

    public void DamageHP(float hp)
    {
        if (_iFrameTimeLeft <= 0.0f)
        {
            _currentHP -= hp;

            if (_currentHP <= 0.0f)
            {
                _currentHP = 0.0f;
                Destroy();
            }
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

    public void ActivateIFrames()
    {
        if (_iFrameTimeLeft <= 0.0f)
        {
            _iFrameTimeLeft = IFramesTime;
        }
    }

    protected abstract void Destroy();
}
