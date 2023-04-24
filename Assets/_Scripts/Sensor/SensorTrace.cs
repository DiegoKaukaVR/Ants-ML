using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTrace : SensorBase
{
    [SerializeField] SensorTraceManager sensorTraceManager;
    TraceManager traceManager;
    TraceGenerator traceGenerator;

    public int idColonyTarget;
    public TraceManager.Info infoObjective;

    protected override void Start()
    {
        base.Start();
        traceManager = TraceManager.instance;
    }

    TraceManager.Trace trace;
    public override bool UpdateSensor()
    {
        trace = sensorTraceManager.updatedTrace;

        if (trace == null)
        {
            return false;
        }
        if (trace.info == infoObjective && trace.idColony == idColonyTarget)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
