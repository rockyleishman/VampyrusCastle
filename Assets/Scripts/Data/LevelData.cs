using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataObject", menuName = "Data/LevelDataObject", order = 0)]
public class LevelData : ScriptableObject
{
    internal PathPoint[] PathPoints;
    internal int Candy;
}
