using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] public LevelData LevelDataObject;
    [SerializeField] public EventData EventDataObject;
    [SerializeField] public PlayerData PlayerDataObject;
}
