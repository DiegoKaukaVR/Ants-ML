using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreState : StateBase
{
   


    public override void OnExecuteState()
    {
        if (entity.myNavmeshAgent.remainingDistance <= 0.2f)
        {
            entity.GoToTarget(entity.FindRandomValidNavmeshPoint());
        }
    }

    public override void OnExitState()
    {
        base.OnExitState();
        entity.StopAgent();
    }
}
