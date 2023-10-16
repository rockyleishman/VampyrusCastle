using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Awake()
    {
        //set candy to 0
        DataManager.Instance.PlayerDataObject.Candy = 0;

        //set time since crystal start to negative to prevent premature spawns
        DataManager.Instance.LevelDataObject.TimeSinceCrystalStart = -1.0f;

        //init crystal charge
        DataManager.Instance.LevelDataObject.CrystalChargePercent = 0.0f;
        HUDManager.Instance.UpdateCrystalCharge();
    }

    public void AddCandy(int candy)
    {
        DataManager.Instance.PlayerDataObject.Candy += candy;
    }

    public void RemoveCandy(int candy)
    {
        DataManager.Instance.PlayerDataObject.Candy -= candy;
    }
}