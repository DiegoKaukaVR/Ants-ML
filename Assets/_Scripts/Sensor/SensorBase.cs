using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBase : MonoBehaviour
{
    protected Character entity;

    protected virtual void Start()
    {
        entity = GetComponentInParent<Character>();
    }
    public virtual bool UpdateSensor()
    {
        Debug.LogError("UpdateSensor not implemented");
        return false;
    }

}
