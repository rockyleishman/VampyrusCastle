using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackEventListener : MonoBehaviour
{
    public AttackEvent AttackEvent;
    public UnityEvent<Vector3, Quaternion, DestructableObject> OnEventTriggered;

    private void OnEnable()
    {
        AttackEvent.AddListener(this);
    }

    private void OnDisable()
    {
        AttackEvent.RemoveListener(this);
    }

    public void OnTriggered(Vector3 attackOrigin, Quaternion attackRotation, DestructableObject attackTarget)
    {
        OnEventTriggered.Invoke(attackOrigin, attackRotation, attackTarget);
    }
}
