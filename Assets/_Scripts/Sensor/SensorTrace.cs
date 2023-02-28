using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTrace : SensorBase
{
    TraceManager traceManager;
    TraceGenerator traceGenerator;

    public TraceManager.Info infoObjective;

    protected override void Start()
    {
        base.Start();
        traceManager = TraceManager.instance;
    }
    public override bool UpdateSensor()
    {
        if (traceManager.CheckTraceProximity(ref entity) == infoObjective)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }

}
