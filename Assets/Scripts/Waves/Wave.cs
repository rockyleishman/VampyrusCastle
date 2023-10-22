using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave : MonoBehaviour
{
    [SerializeField] public float WaveTimeLimit;
    [SerializeField] public LocalWave[] LocalWaves;
}
