using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTransition : TransitionBase
{
    public SensorBase sensor;

    public override bool CheckTransition()
    {
        return sensor.UpdateSensor();
    }
}
