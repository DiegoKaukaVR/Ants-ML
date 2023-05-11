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

    public bool enemy;

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
            if (enemy)
            {
                ant.Target = trace.ant.transform;
                    return true;
            }
            if (trace.Target != null)
            {
                ant.Target = trace.Target;

            }
            return true;

        }
        else
        {
            return false;
        }
    }
}
