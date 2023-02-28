using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTransition : TransitionBase
{
    public Resource resource;
    public GatherResourceState resourceState;

    public override void OnEnterStateTransition()
    {
        resource = resourceState.currentResource;
    }
    public override bool CheckTransition()
    {
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
