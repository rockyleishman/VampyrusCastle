using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    void Start()
    {
        DataManager.Instance.LevelDataObject.PathPoints = GetComponentsInChildren<PathPoint>();
    }
}
