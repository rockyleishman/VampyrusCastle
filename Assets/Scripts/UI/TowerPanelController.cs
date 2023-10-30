using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerPanelController : MonoBehaviour
{
    public TextMeshProUGUI basicTMP;
    public TextMeshProUGUI healthTMP;
    public TextMeshProUGUI iceTMP;
    public TextMeshProUGUI bombTMP;
    public Image towerAImage;
    public Image towerBImage;
    public Image towerCImage;
    public Image towerDImage;
    
    // Start is called before the first frame update
    void Start()
    {
        InitialImageColor();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTowerColor(towerAImage, 10);
        ChangeTowerColor(towerBImage, 15);
        ChangeTowerColor(towerCImage, 20);
        ChangeTowerColor(towerDImage, 25);

    }

    void ChangeTowerColor(Image image, int amount)
    {
        if(DataManager.Instance.PlayerDataObject.Candy>=amount)
        {
            image.color = new Color(1f, 1f, 1f);
        }
        else
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }

    void InitialImageColor()
    {
        towerAImage.color = new Color(0.5f, 0.5f, 0.5f);
        towerBImage.color = new Color(0.5f, 0.5f, 0.5f);
        towerCImage.color = new Color(0.5f, 0.5f, 0.5f);
        towerDImage.color = new Color(0.5f, 0.5f, 0.5f);

    }
}
