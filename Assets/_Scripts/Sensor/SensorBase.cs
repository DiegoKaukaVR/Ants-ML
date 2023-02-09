using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorBase : MonoBehaviour
{
    public virtual bool UpdateSensor()
    {
        Debug.LogError("UpdateSensor not implemented");
        return false;
    }

}
