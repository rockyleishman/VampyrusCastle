using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : DestructableObject
{
    [Header("Crystal Settings")]
    [SerializeField] public float CrystalActivationRadius = 3.0f;
    [SerializeField] [Range(0.1f, 50.0f)] public float InitialChargePercent = 0.1f;
    [SerializeField] public float ChargeTime = 300.0f;

    private SpriteRenderer _spriteRenderer;
    private bool _isCharging;
    private bool _isFinished;

    protected override void Start()
    {
        base.Start();

        //set crystal reference in level data
        DataManager.Instance.LevelDataObject.Crystal = this;

        //set initial HP based on charge percent
        _currentHP = InitialChargePercent * MaxHP / 100.0f;

        //get sprite renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();

        //set colour to grey
        _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);

        //does not charge at game start
        _isCharging = false;
        _isFinished = false;
    }

    protected override void Update()
    {
        base.Update();

        if (_isCharging)
        {
            //increase charge/HP
            HealHP((MaxHP - (InitialChargePercent * MaxHP / 100.0f)) * Time.deltaTime / ChargeTime);
            DataManager.Instance.LevelDataObject.CrystalChargePercent = _currentHP * 100.0f / MaxHP;
            HUDManager.Instance.UpdateCrystalCharge();

            //increase charge time
            DataManager.Instance.LevelDataObject.TimeSinceCrystalStart += Time.deltaTime;

            //check for complete charge
            if (_currentHP >= MaxHP)
            {
                //finish charging
                _isCharging = false;
                _isFinished = true;

                //trigger end event
                DataManager.Instance.EventDataObject.CrystalFinish.TriggerEvent(transform.position);

                Debug.Log("The crystal has fully charged! Gate opened/level complete!");
            }
        }
    }

    public override void DamageHP(float hp)
    {
        base.DamageHP(hp);

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
        if(!_isFinished && !_isCharging && Vector3.Distance(transform.position, DataManager.Instance.PlayerDataObject.Player.transform.position) <= CrystalActivationRadius)
        {
            _isCharging = true;

            //change colour
            _spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            //show crystal charge HUD
            HUDManager.Instance.ShowCrystalCharge();
        }        
    }
}
