using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataObject", menuName = "LevelDataObject", order = 50)]
public class LevelData : ScriptableObject
{
    internal PathPoint[] PathPoints;
}
