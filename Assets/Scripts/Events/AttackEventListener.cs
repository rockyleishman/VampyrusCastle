using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackEventListener : MonoBehaviour
{
    public AttackEvent AttackEvent;
    public UnityEvent<Vector3, Vector3, DestructableObject> OnEventTriggered;

    private void OnEnable()
    {
        AttackEvent.AddListener(this);
    }

    private void OnDisable()
    {
        AttackEvent.RemoveListener(this);
    }

    public void OnTriggered(Vector3 attackOrigin, Vector3 attackDirection, DestructableObject attackTarget)
    {
        OnEventTriggered.Invoke(attackOrigin, attackDirection, attackTarget);
    }
}
