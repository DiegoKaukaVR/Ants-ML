using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBase : MonoBehaviour
{
    protected Ant entity;

    protected virtual void Start()
    {
        entity = GetComponentInParent<Ant>();
    }
    public virtual bool UpdateSensor()
    {
        Debug.LogError("UpdateSensor not implemented");
        return false;
    }

}
