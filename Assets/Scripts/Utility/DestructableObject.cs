using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableObject : PoolObject
{
    [Header("Health")]
    [SerializeField] public float MaxHP = 100;
    protected float _currentHP;
    [SerializeField][Range(0.0f, 10.0f)] public float KnockbackMultiplier = 1.0f;
    private Vector3 _knockbackRemaining;
    private float _knockbackTimeRemaining;
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

        //apply knockback
        if (_knockbackTimeRemaining > 0.0f)
        {
            Vector3 knockbackThisFrame = _knockbackRemaining / _knockbackTimeRemaining * Time.deltaTime;
            transform.Translate(knockbackThisFrame);
            _knockbackRemaining -= knockbackThisFrame;
            _knockbackTimeRemaining -= Time.deltaTime;
        }
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
                DestroyObject();
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

    public void Knockback(Vector3 knockback, float time)
    {
        _knockbackRemaining = knockback * KnockbackMultiplier;
        _knockbackTimeRemaining = time;
    }

    public void ActivateIFrames()
    {
        if (_iFrameTimeLeft <= 0.0f)
        {
            _iFrameTimeLeft = IFramesTime;
        }
    }

    protected abstract void DestroyObject();
}
