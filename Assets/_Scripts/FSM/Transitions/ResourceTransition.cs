using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTransition : SensorBase
{
    public Resource resource;
    public GatherResourceState resourceState;

    public override void OnEnterStateTransition()
    {
        resource = resourceState.currentResource;
    }
    public override bool UpdateSensor()
    {
        if (resource == null)
        {
            return true;
        }
        if (resource.Quantity <= 0 && !resourceState.gathering)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
