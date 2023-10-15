using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{
    internal TowerNodeController[] TowerNodes;
    private TowerNodeController _currentNode;
    internal bool IsInBuildMode;

    [SerializeField] public float BuildMenuAreaRadius = 3.0f;

    [Header("BuildModeHotKeys")]
    [SerializeField] public KeyCode DemolishHotKey;
    [SerializeField] public KeyCode[] HotKeysByLevel1TowerIndex;
    [SerializeField] public KeyCode[] HotKeysByLevel2TowerIndex;

    [Header("Events")]
    [SerializeField] public GameEvent ShowLevel0BuildMenu;
    [SerializeField] public GameEvent ShowLevel1BuildMenu;
    [SerializeField] public GameEvent ShowLevel2BuildMenu;
    [SerializeField] public GameEvent HideBuildMenus;

    private void Start()
    {
        //init tower nodes
        TowerNodes = GetComponentsInChildren<TowerNodeController>();
        _currentNode = null;

        //start out of build mode
        IsInBuildMode = false;
    }

    private void Update()
    {
        if (IsInBuildMode)
        {
            //check if player is still in range
            if (Vector3.Distance(_currentNode.transform.position, DataManager.Instance.PlayerDataObject.Player.transform.position) <= BuildMenuAreaRadius)
            {
                BuildMode();
            }
            else
            {
                ExitBuildMode();
            }
        }
    }

    public void EnterBuildMode(Vector3 playerLocation)
    {
        //find nearest node
        foreach (TowerNodeController node in TowerNodes)
        {
            if (_currentNode == null || Vector3.Distance(node.transform.position, playerLocation) < Vector3.Distance(_currentNode.transform.position, playerLocation))
            {
                _currentNode = node;
            }
        }

        //check if node is within range
        if (_currentNode != null && Vector3.Distance(_currentNode.transform.position, playerLocation) <= BuildMenuAreaRadius)
        {
            //start build mode
            IsInBuildMode = true;

            //TODO: highlight targetnode's towers

            //open build menu UI
            switch (_currentNode.ActiveTowerLevel)
            {
                case 0:
                    ShowLevel0BuildMenu.TriggerEvent(_currentNode.transform.position);
                    Debug.Log(string.Format("BUILD MODE: [{0}] - {1}", HotKeysByLevel1TowerIndex[0], _currentNode.Level1Towers[0].name));
                    break;

                case 1:
                    ShowLevel1BuildMenu.TriggerEvent(_currentNode.transform.position);
                    Debug.Log(string.Format("BUILD MODE: [{0}] - {1}   |||   [{2}] - {3}   |||   [{4}] - {5}   |||   [{6}] - Demolish Tower", HotKeysByLevel2TowerIndex[0], _currentNode.Level2Towers[0].name, HotKeysByLevel2TowerIndex[1], _currentNode.Level2Towers[1].name, HotKeysByLevel2TowerIndex[2], _currentNode.Level2Towers[2].name, DemolishHotKey));
                    break;

                case 2:
                    ShowLevel2BuildMenu.TriggerEvent(_currentNode.transform.position);
                    Debug.Log(string.Format("BUILD MODE: [{0}] - Demolish Tower", DemolishHotKey));
                    break;

                default:
                    //no menu
                    break;
            }
        }
        //else nothing happens
    }

    public void ExitBuildMode()
    {
        IsInBuildMode = false;

        //TODO: unhighlight targetnode's towers

        //hide build menu UI
        HideBuildMenus.TriggerEvent(_currentNode.transform.position);
        Debug.Log("BUILD MODE CLOSED");
    }

    private void BuildMode()
    {
        //build tower
        switch (_currentNode.ActiveTowerLevel)
        {
            case 0:
                for (int i = 0; i < HotKeysByLevel1TowerIndex.Length; i++)
                {
                    if (Input.GetKeyDown(HotKeysByLevel1TowerIndex[i]))
                    {
                        _currentNode?.ActivateTower(1, i);
                        ExitBuildMode();
                    }
                }
                break;

            case 1:
                for (int i = 0; i < HotKeysByLevel2TowerIndex.Length; i++)
                {
                    if (Input.GetKeyDown(HotKeysByLevel2TowerIndex[i]))
                    {
                        _currentNode?.ActivateTower(2, i);
                        ExitBuildMode();
                    }
                }
                break;

            case 2:
                //no builds after level 2
                break;

            default:
                ExitBuildMode();
                break;
        }

        //demolish tower
        if (Input.GetKeyDown(DemolishHotKey))
        {
            _currentNode.ResetTower();
            ExitBuildMode();
        }
    }
}