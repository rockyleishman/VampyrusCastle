using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalController : DestructableObject
{
    [Header("Crystal Settings")]
    [SerializeField] public Effect CrystalFinishEffect;
    private Slider _slider;
    private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _slider = GetComponentInChildren<Slider>();
        //set crystal reference in level data
        DataManager.Instance.LevelDataObject.Crystal = this;

        //set initial HP
        DataManager.Instance.LevelDataObject.CrystalHP = DataManager.Instance.LevelDataObject.MaxCrystalHP;
        _currentHP = DataManager.Instance.LevelDataObject.CrystalHP;
        //HUDManager.Instance.UpdateCrystalHP();

        //get sprite renderer and particle system
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _particleSystem = GetComponent<ParticleSystem>();

        //set colour to greyish
        _spriteRenderer.color = new Color(0.75f, 0.75f, 0.75f, 1.0f);

        //pause particle system
        _particleSystem.Pause();
    }

    protected override void Update()
    {
        base.Update();
        _slider.value = _currentHP;
        _spriteRenderer.color = Color.Lerp(new Color(1.0f, 1.0f, 1.0f, 1.0f), new Color(0.0f, 1.0f, 1.0f, 1.0f), (float)WaveManager.Instance.CurrentWave / (float)DataManager.Instance.LevelDataObject.WaveCount);
    }

    public override void DamageHP(float hp)
    {
        base.DamageHP(hp);
        DataManager.Instance.LevelDataObject.CrystalHP = _currentHP;

        if (_iFrameTimeLeft <= 0.0f)
        {
            DataManager.Instance.EventDataObject.CrystalDamage.TriggerEvent(transform.position);
        }
    }

    protected override void DestroyObject()
    {
        DataManager.Instance.EventDataObject.CrystalDeath.TriggerEvent(transform.position);

        Debug.Log("GAME OVER (Crystal Destroyed)");
    }

    public void StartCharging()
    {
        //change colour
        _spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //play particle system
        _particleSystem.Play();
    }

    public void FinishCharging()
    {
        //pause particle system
        _particleSystem.Pause();

        //spawn effect
        PoolManager.Instance.Spawn(CrystalFinishEffect.name, transform.position, Quaternion.identity);
    }
}
