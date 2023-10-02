using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aura : MonoBehaviour
{
    private List<DestructableObject> AffectedObjects;

    private void Start()
    {
        //init affected objects list
        AffectedObjects = new List<DestructableObject>();
    }

    private void Update()
    {
        //remove statuses of objects that are dead, disabled, or destroyed
        foreach (DestructableObject destructableObject in AffectedObjects)
        {
            if (destructableObject == null || !destructableObject.isActiveAndEnabled || !destructableObject.IsAlive)
            {
                RemoveStatus(destructableObject);
            }
        }
    }

    public void ApplyStatus(DestructableObject destructableObject)
    {
        //add to list of affected objects so status can be removed on disable / on destroy
        AffectedObjects.Add(destructableObject);
        //apply the status
        StatusApplication(destructableObject);
    }

    protected abstract void StatusApplication(DestructableObject destructableObject);

    public void RemoveStatus(DestructableObject destructableObject)
    {
        //remove the affected object from the list
        if (AffectedObjects.Remove(destructableObject))
        {
            //remove the status
            StatusRemoval(destructableObject);
        }
    }

    protected abstract void StatusRemoval(DestructableObject destructableObject);

    private void OnDisable()
    {
        //remove all statuses if aura is disabled
        foreach (DestructableObject destructableObject in AffectedObjects)
        {
            RemoveStatus(destructableObject);
        }
    }

    private void OnDestroy()
    {
        //remove all statuses if aura is destroyed
        foreach (DestructableObject destructableObject in AffectedObjects)
        {
            RemoveStatus(destructableObject);
        }
    }
}
