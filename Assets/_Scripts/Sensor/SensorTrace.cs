using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTrace : SensorBase
{
    TraceManager traceManager;
    TraceGenerator traceGenerator;

    protected override void Start()
    {
        base.Start();
        traceManager = TraceManager.instance;
    }
    public override bool UpdateSensor()
    {
        return traceManager.CheckTraceProximity(entity);
    }

}
