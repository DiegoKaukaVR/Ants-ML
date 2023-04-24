using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorTraceManager : MonoBehaviour
{
    public TraceManager.Trace updatedTrace;
    public Character ant;

    private void Update()
    {
        updatedTrace = TraceManager.instance.CheckTraceProximity(ref ant);
    }

}
