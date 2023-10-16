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

    [Header("Sliding Bars")]
    [SerializeField] public GameObject HPBarFill;
    [SerializeField] public GameObject[] HPBarEmpty;
    [SerializeField] public GameObject CandyBarFill;
    [SerializeField] public GameObject[] CandyBarEmpty;
    [SerializeField] public GameObject CrystalChargeBarFill;
    [SerializeField] public GameObject[] CrystalChargeBarEmpty;

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
        //set text
        HPField.SetText(Mathf.CeilToInt(DataManager.Instance.PlayerDataObject.CurrentHP).ToString());

        //set bar
        HPBarFill.transform.localScale = new Vector3(Mathf.Clamp01(DataManager.Instance.PlayerDataObject.CurrentHP / DataManager.Instance.PlayerDataObject.MaxHP), 1.0f, 1.0f);
        foreach (GameObject barEmpty in HPBarEmpty)
        {
            barEmpty.transform.localScale = new Vector3(Mathf.Clamp01((DataManager.Instance.PlayerDataObject.MaxHP - DataManager.Instance.PlayerDataObject.CurrentHP) / DataManager.Instance.PlayerDataObject.MaxHP), 1.0f, 1.0f);
        }
    }

    public void UpdateCandy()
    {
        //set text
        CandyField.SetText(DataManager.Instance.PlayerDataObject.Candy.ToString());

        //set bar
        CandyBarFill.transform.localScale = new Vector3(Mathf.Clamp01((float)DataManager.Instance.PlayerDataObject.Candy / (float)DataManager.Instance.LevelDataObject.VisualMaxCandy), 1.0f, 1.0f);
        foreach (GameObject barEmpty in CandyBarEmpty)
        {
            barEmpty.transform.localScale = new Vector3(Mathf.Clamp01(((float)DataManager.Instance.LevelDataObject.VisualMaxCandy - (float)DataManager.Instance.PlayerDataObject.Candy) / (float)DataManager.Instance.LevelDataObject.VisualMaxCandy), 1.0f, 1.0f);
        }
    }

    public void UpdateCrystalCharge()
    {
        //set text
        CrystalChargeField.SetText(Mathf.FloorToInt(DataManager.Instance.LevelDataObject.CrystalChargePercent).ToString());

        //set bar
        CrystalChargeBarFill.transform.localScale = new Vector3(Mathf.Clamp01(DataManager.Instance.LevelDataObject.CrystalChargePercent / 100.0f), 1.0f, 1.0f);
        foreach (GameObject barEmpty in CrystalChargeBarEmpty)
        {
            barEmpty.transform.localScale = new Vector3(Mathf.Clamp01((100.0f - DataManager.Instance.LevelDataObject.CrystalChargePercent) / 100.0f), 1.0f, 1.0f);
        }
    }
}
