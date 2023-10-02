using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSpawner : MonoBehaviour
{
    public abstract void TriggerAttack(Vector3 attackOrigin, Vector3 attackDirection, DestructableObject attackTarget);
}
