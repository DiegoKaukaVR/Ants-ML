using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResourceState : StateBase
{
   
    public Transform Home;

    public Transform headAnt;
    public GameObject resourcePrefab;

    bool haveResource;

    bool gathering;

    public override void OnEnterState()
    {
        base.OnEnterState();
        entity.GoToTarget(entity.Target.position);
    }
    public override void OnExecuteState()
    {
        if (entity.myNavmeshAgent.remainingDistance<0.1f)
        {
            if (gathering)
            {
                Resource resource = entity.Target.GetComponent<Resource>();
                resource.GatherResource();
             
                /// COMO PUEDO ANALIZAR SI HA TERMINADO DE HACER GATHER RESOURCE?
                entity.GoToTarget(entity.Target.position);

                if (haveResource)
                {
                    RemoveResource();
                }
                gathering = false;
            }
            else
            {
                entity.GoToTarget(Home.position);
                TakeResource();
             
                gathering = true;
            }
        }
        
    }

    GameObject resourcePiece;

    void TakeResource()
    {
        resourcePiece = Instantiate(resourcePrefab, headAnt.position, Quaternion.identity, headAnt);
        haveResource = true;
    }

    void RemoveResource()
    {
        haveResource = false;
        resourcePiece.SetActive(false);
        resourcePiece = null;
    }
}
