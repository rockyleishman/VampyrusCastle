using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [SerializeField] public PathPoint NextPoint;
    [SerializeField] public bool IsPathEnd = false;
}
