using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPoint : PathPoint
{
    [SerializeField][Range(-1, 1)] int XOffset = 0;
    [SerializeField][Range(-1, 1)] int YOffset = 0;

    private const float k_OverlapBoxSize = 0.1f;

    private void Start()
    {
        //find next path point based on offset
        NextPoint = Physics2D.OverlapBox(transform.position + new Vector3(XOffset, YOffset, 0.0f), Vector2.one * k_OverlapBoxSize, 0.0f, LayerMask.GetMask("PathPoint")).GetComponent<PathPoint>();
    }
}
