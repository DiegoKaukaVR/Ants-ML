using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBase : MonoBehaviour
{
    protected Character ant;

    protected virtual void Start()
    {
        ant = GetComponentInParent<Character>();
    }

    public virtual void OnEnterStateTransition()
    {

    }
    public virtual bool UpdateSensor()
    {
        Debug.LogError("UpdateSensor not implemented");
        return false;
    }

}
