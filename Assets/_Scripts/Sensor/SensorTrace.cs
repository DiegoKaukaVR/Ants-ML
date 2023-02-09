using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTrace : SensorBase
{
    TraceManager traceManager;
    Ant ant;
    TraceGenerator traceGenerator;

    private void Start()
    {
        traceManager = TraceManager.instance;
    }
    public override bool UpdateSensor()
    {
        return traceManager.CheckTraceProximity(ant);
    }

    //private void Update()
    //{
    //    
    //}



}
