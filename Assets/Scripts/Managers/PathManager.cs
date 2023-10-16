using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    private void Start()
    {
        //disable rendering
        GetComponent<Renderer>().sortingOrder = -10000;
        GetComponent<Renderer>().forceRenderingOff = true;

        //load all path points into level data
        DataManager.Instance.LevelDataObject.PathPoints = GetComponentsInChildren<PathPoint>();

        //init flow points
        StartCoroutine(InitFlowPointsCoroutine());
    }

    private IEnumerator InitFlowPointsCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (FlowPoint flowPoint in DataManager.Instance.LevelDataObject.PathPoints)
        {
            flowPoint.Init();
        }
    }
}
