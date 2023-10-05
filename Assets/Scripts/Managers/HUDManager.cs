using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : Singleton<HUDManager>
{
    [Header("Groups")]
    [SerializeField] public GameObject HPGroup;
    [SerializeField] public GameObject CandyGroup;
    [SerializeField] public GameObject CrystalChargeGroup;

    [Header("Text Fields")]
    [SerializeField] public TextMeshProUGUI HPField;
    [SerializeField] public TextMeshProUGUI CandyField;
    [SerializeField] public TextMeshProUGUI CrystalChargeField;

    private void Start()
    {
        ShowHP();
        ShowCandy();
        HideCrystalCharge();
    }

    public void ShowHUD()
    {
        this.gameObject.SetActive(true);
    }

    public void HideHUD()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowHP()
    {
        HPGroup.SetActive(true);
    }

    public void HideHP()
    {
        HPGroup.SetActive(false);
    }

    public void ShowCandy()
    {
        CandyGroup.SetActive(true);
    }

    public void HideCandy()
    {
        CandyGroup.SetActive(false);
    }

    public void ShowCrystalCharge()
    {
        CrystalChargeGroup.SetActive(true);
    }

    public void HideCrystalCharge()
    {
        CrystalChargeGroup.SetActive(false);
    }

    public void UpdateHP()
    {
        HPField.text = "" + Mathf.CeilToInt(DataManager.Instance.PlayerDataObject.CurrentHP);
    }

    public void UpdateCandy()
    {
        CandyField.SetText(DataManager.Instance.PlayerDataObject.Candy.ToString());
    }

    public void UpdateCrystalCharge()
    {
        //TODO: implement crystal charging//
    }
}
