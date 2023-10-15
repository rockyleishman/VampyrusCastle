using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataObject", menuName = "Data/PlayerDataObject", order = 0)]
public class PlayerData : ScriptableObject
{
    internal PlayerController Player;
    internal float MaxHP;
    internal float CurrentHP;
    internal int Candy;
}
