using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : Singleton<HUDManager>
{
    [Header("Groups")]
    //[SerializeField] public GameObject HPGroup;
    //[SerializeField] public GameObject CrystalHPGroup;
    [SerializeField] public GameObject CandyGroup;
    [SerializeField] public GameObject WaveGroup;
    [SerializeField] public GameObject TimeGroup;
    [SerializeField] public GameObject EnemiesGroup;
    [SerializeField] public GameObject GameOverGroup;
    [Header("Text Fields")]
    //[SerializeField] public TextMeshProUGUI HPField;
    //[SerializeField] public TextMeshProUGUI CrystalHPField;
    [SerializeField] public TextMeshProUGUI CandyField;
    [SerializeField] public TextMeshProUGUI WaveField;
    [SerializeField] public TextMeshProUGUI TimeField;
    [SerializeField] public TextMeshProUGUI EnemiesField;

    [Header("Sliding Bars")]
    //[SerializeField] public GameObject HPBarFill;
    //[SerializeField] public GameObject[] HPBarEmpty;
    //[SerializeField] public GameObject CrystalHPBarFill;
    //[SerializeField] public GameObject[] CrystalHPBarEmpty;
    [SerializeField] public GameObject CandyBarFill;
    [SerializeField] public GameObject[] CandyBarEmpty;
    [SerializeField] public GameObject WaveBar;
    [SerializeField] public Color WaveBarSignalColour;
    [SerializeField] public Color WaveBarDefaultColour;
    [SerializeField] public GameObject TimeBar;
    [SerializeField] public Color TimeBarSignalColour;
    [SerializeField] public Color TimeBarDefaultColour;
    [SerializeField] public GameObject EnemiesBarFill;
    [SerializeField] public GameObject[] EnemiesBarEmpty;
    
    private void Start()
    {
        //ShowHP();
        //ShowCrystalHP();
        ShowCandy();
        HideWave();
        HideTime();
        HideEnemies();
        HideGameOver();
    }

    private void Update()
    {
        if (DataManager.Instance.PlayerDataObject.CurrentHP <= 0||DataManager.Instance.LevelDataObject.CrystalHP<=0)
        {
            ShowGameOver();
            Time.timeScale = 0;
        }
    }

    public void ShowHUD()
    {
        gameObject.SetActive(true);
    }

    public void HideHUD()
    {
        gameObject.SetActive(false);
    }

    public void ShowAll()
    {
        //ShowHP();
        //ShowCrystalHP();
        ShowCandy();
        ShowWave();
        ShowTime();
        ShowEnemies();
    }

    public void HideAll()
    {
        //HideHP();
        //HideCrystalHP();
        HideCandy();
        HideWave();
        HideTime();
        HideEnemies();
    }

    /*public void ShowHP()
    {
        HPGroup.SetActive(true);
    }

    public void HideHP()
    {
        HPGroup.SetActive(false);
    }*/

    /*public void ShowCrystalHP()
    {
        CrystalHPGroup.SetActive(true);
    }

    public void HideCrystalHP()
    {
        CrystalHPGroup.SetActive(false);
    }*/

    public void ShowCandy()
    {
        CandyGroup.SetActive(true);
    }

    public void HideCandy()
    {
        CandyGroup.SetActive(false);
    }
    
    public void ShowGameOver()
    {
        GameOverGroup.SetActive(true);
    }
    public void HideGameOver()
    {
        GameOverGroup.SetActive(false);
    }

    public void ShowWave()
    {
        WaveGroup.SetActive(true);
    }

    public void HideWave()
    {
        WaveGroup.SetActive(false);
    }

    public void ShowTime()
    {
        TimeGroup.SetActive(true);
    }

    public void HideTime()
    {
        TimeGroup.SetActive(false);
    }

    public void ShowEnemies()
    {
        EnemiesGroup.SetActive(true);
    }

    public void HideEnemies()
    {
        EnemiesGroup.SetActive(false);
    }

    /*public void UpdateHP()
    {
        //set text
        //HPField.SetText(Mathf.CeilToInt(DataManager.Instance.PlayerDataObject.CurrentHP).ToString());

        //set bar
        HPBarFill.transform.localScale = new Vector3(Mathf.Clamp01(DataManager.Instance.PlayerDataObject.CurrentHP / DataManager.Instance.PlayerDataObject.MaxHP), 1.0f, 1.0f);
        foreach (GameObject barEmpty in HPBarEmpty)
        {
            barEmpty.transform.localScale = new Vector3(Mathf.Clamp01((DataManager.Instance.PlayerDataObject.MaxHP - DataManager.Instance.PlayerDataObject.CurrentHP) / DataManager.Instance.PlayerDataObject.MaxHP), 1.0f, 1.0f);
        }
    }*/

    /*public void UpdateCrystalHP()
    {
        //set text
        //CrystalHPField.SetText(Mathf.FloorToInt(DataManager.Instance.LevelDataObject.CrystalHP).ToString());

        //set bar
        CrystalHPBarFill.transform.localScale = new Vector3(Mathf.Clamp01(DataManager.Instance.LevelDataObject.CrystalHP / DataManager.Instance.LevelDataObject.MaxCrystalHP), 1.0f, 1.0f);
        foreach (GameObject barEmpty in CrystalHPBarEmpty)
        {
            barEmpty.transform.localScale = new Vector3(Mathf.Clamp01((DataManager.Instance.LevelDataObject.MaxCrystalHP - DataManager.Instance.LevelDataObject.CrystalHP) / DataManager.Instance.LevelDataObject.MaxCrystalHP), 1.0f, 1.0f);
        }
    }*/

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

    public void UpdateWave()
    {
        //set text
        WaveField.SetText(WaveManager.Instance.CurrentWave.ToString() + " / " + DataManager.Instance.LevelDataObject.WaveCount);
    }

    public void UpdateTime()
    {        
        if (WaveManager.Instance.IsSpawningCompleted)
        {
            //set text
            TimeField.SetText("-");

            //set bar colour
            //EnemiesBarFill.GetComponent<Image>().material.color = TimeBarDefaultColour;
        }
        else
        {
            //set text
            TimeField.SetText(Mathf.FloorToInt(DataManager.Instance.LevelDataObject.TimeToNextWave).ToString());

            //set bar colour
            //EnemiesBarFill.GetComponent<Image>().material.color = Color.Lerp(TimeBarDefaultColour, TimeBarSignalColour, Mathf.Clamp01((DataManager.Instance.LevelDataObject.WaveTimeWarning - DataManager.Instance.LevelDataObject.TimeToNextWave) / DataManager.Instance.LevelDataObject.WaveTimeWarning));
        }

        
    }

    public void UpdateEnemies()
    {
        //set text
        EnemiesField.SetText(DataManager.Instance.LevelDataObject.EnemiesRemaining.ToString());

        //set bar
        EnemiesBarFill.transform.localScale = new Vector3(Mathf.Clamp01((float)DataManager.Instance.LevelDataObject.EnemiesRemaining / (float)DataManager.Instance.LevelDataObject.VisualMaxEnemies), 1.0f, 1.0f);
        foreach (GameObject barEmpty in EnemiesBarEmpty)
        {
            barEmpty.transform.localScale = new Vector3(Mathf.Clamp01(((float)DataManager.Instance.LevelDataObject.VisualMaxEnemies - (float)DataManager.Instance.LevelDataObject.EnemiesRemaining) / (float)DataManager.Instance.LevelDataObject.VisualMaxEnemies), 1.0f, 1.0f);
        }
    }
}
