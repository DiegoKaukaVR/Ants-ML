using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResourceState : StateBase
{
    public Transform Resource;
    public Transform Home;

    bool gathering;

    public override void OnEnterState()
    {
        base.OnEnterState();
        entity.GoToTarget(Resource.position);
    }
    public override void OnExecuteState()
    {
        if (entity.myNavmeshAgent.remainingDistance<0.1f)
        {
            if (gathering)
            {
                Resource resource = Resource.GetComponent<Resource>();
                resource.GatherResource();

                /// COMO PUEDO ANALIZAR SI HA TERMINADO DE HACER GATHER RESOURCE?
                entity.GoToTarget(Resource.position);
                
                gathering = false;
            }
            else
            {
                entity.GoToTarget(Home.position);
                gathering = true;
            }
        }
        
    }
}
