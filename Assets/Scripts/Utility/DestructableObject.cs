using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableObject : PoolObject
{
    [Header("Health")]
    [SerializeField] public float MaxHP = 100;
    protected float _currentHP;
    internal bool IsAlive;
    [SerializeField][Range(0.0f, 10.0f)] public float KnockbackMultiplier = 1.0f;
    private Vector3 _knockbackRemaining;
    private float _knockbackTimeRemaining;
    [SerializeField] public float IFramesTime = 0.5f;
    protected float _iFrameTimeLeft;
    private const float k_flashTime = 0.1f;
    private const float k_flashColorMultiplier = 2.0f;

    //status variables
    internal float SpeedMultiplier;
    internal float HealingPerSecond;

    //renderer stuff
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    protected virtual void Start()
    {
        //get renderer stuff
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            defaultColor = spriteRenderer.material.color;
        }

        //initialize
        Init();
    }

    protected void Init()
    {
        InitHP();
        InitStatuses();

        if (spriteRenderer != null)
        {
            spriteRenderer.material.color = defaultColor;
        }
    }

    protected virtual void InitHP()
    {
        _currentHP = MaxHP;
        _iFrameTimeLeft = 0.0f;
        IsAlive = true;
    }

    protected void InitStatuses()
    {
        SpeedMultiplier = 1.0f;
        HealingPerSecond = 0.0f;
    }

    protected virtual void Update()
    {
        //decrease i frame time left
        _iFrameTimeLeft -= Time.deltaTime * SpeedMultiplier;

        //apply knockback
        if (_knockbackTimeRemaining > 0.0f)
        {
            Vector3 knockbackThisFrame = _knockbackRemaining / _knockbackTimeRemaining * Time.deltaTime * SpeedMultiplier;
            transform.Translate(knockbackThisFrame);
            _knockbackRemaining -= knockbackThisFrame;
            _knockbackTimeRemaining -= Time.deltaTime * SpeedMultiplier;
        }

        //apply heal over time
        HealHP(HealingPerSecond * Time.deltaTime * HealingPerSecond);
    }

    public virtual void DamageHP(float hp)
    {
        if (_iFrameTimeLeft <= 0.0f)
        {
            _currentHP -= hp;

            //flash effect
            StartCoroutine(FlashEffect());

            //destroy if 0 HP
            if (_currentHP <= 0.0f)
            {
                _currentHP = 0.0f;
                IsAlive = false;
                DestroyObject();
            }
        }
    }

    public virtual void HealHP(float hp)
    {
        _currentHP += hp;

        if (_currentHP > MaxHP)
        {
            _currentHP = MaxHP;
        }
    }

    public void Knockback(Vector3 knockback, float time)
    {
        if (_iFrameTimeLeft <= 0.0f)
        {
            _knockbackRemaining = knockback * KnockbackMultiplier;
            _knockbackTimeRemaining = time;
        }        
    }

    public void ActivateIFrames()
    {
        if (_iFrameTimeLeft <= 0.0f)
        {
            _iFrameTimeLeft = IFramesTime;
        }
    }

    protected abstract void DestroyObject();

    private IEnumerator FlashEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material.color *= k_flashColorMultiplier;

            yield return new WaitForSeconds(k_flashTime);

            spriteRenderer.material.color /= k_flashColorMultiplier;
        }        
    }
}
