using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataObject", menuName = "Data/SoundDataObject", order = 5)]
public class SoundData : ScriptableObject
{
    public AudioClip beginSound;

    public AudioClip GameSound;

    public AudioClip GameOverSound;

    public AudioClip hitSound;

}
