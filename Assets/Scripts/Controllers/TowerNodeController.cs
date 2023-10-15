using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerNodeController : MonoBehaviour
{
    [Header("Tower Prefabs")]
    [SerializeField] public TowerAIController EmptyNode;
    [SerializeField] public TowerAIController[] Level1Towers;
    [SerializeField] public TowerAIController[] Level2Towers;

    [Header("Starting Tower")]
    [SerializeField] [Range(0, 2)] public int StartingTowerLevel = 0;
    [SerializeField] [Range(0, 15)] public int StartingTowerIndex = 0;

    internal TowerAIController ActiveTower;
    internal int ActiveTowerLevel;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ActivateTower(0, 0);
    }

    private void DeactivateTowers()
    {
        //disable and deactivate towers
        EmptyNode.enabled = false;
        EmptyNode.gameObject.SetActive(false);

        foreach (TowerAIController tower in Level1Towers)
        {
            tower.enabled = false;
            tower.gameObject.SetActive(false);
        }

        foreach (TowerAIController tower in Level2Towers)
        {
            tower.enabled = false;
            tower.gameObject.SetActive(false);
        }
    }

    public void ResetTower()
    {
        ActivateTower(0, 0);
    }

    public void ActivateTower(int level, int index)
    {
        DeactivateTowers();

        //set active tower
        if (level == 0)
        {
            ActiveTower = EmptyNode;
            ActiveTowerLevel = 0;
        }
        else if (level == 1)
        {
            ActiveTower = Level1Towers[index];
            ActiveTowerLevel = 1;
        }
        else if (level == 2)
        {
            ActiveTower = Level2Towers[index];
            ActiveTowerLevel = 2;
        }
        else
        {
            throw new System.Exception("Tower Level " + level + " does not exist!");
        }        

        //activate and enable active tower
        ActiveTower.gameObject.SetActive(true);
        ActiveTower.enabled = true;
    }
}
