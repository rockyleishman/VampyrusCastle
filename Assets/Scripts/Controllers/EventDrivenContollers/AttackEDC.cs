using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackEDC : MonoBehaviour
{
    public abstract void TriggerAttack(Vector3 attackOrigin, Quaternion attackRotation, DestructableObject attackTarget);
}
