using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherResourceState : StateBase
{
   
    public Transform Home;

    public Transform headAnt;
    public GameObject resourcePrefab;

    public Resource currentResource;

    bool haveResource;

    public bool gathering;

    public override void OnEnterState()
    {
        base.OnEnterState();
        currentResource = entity.Target.GetComponent<Resource>();
        entity.GoToTarget(entity.Target.position);
    }
    public override void OnExecuteState()
    {
        if (entity.myNavmeshAgent.remainingDistance<0.1f)
        {
            if (gathering)
            {
                /// COMO PUEDO ANALIZAR SI HA TERMINADO DE HACER GATHER RESOURCE?
                entity.GoToTarget(entity.Target.position);

                if (haveResource)
                {
                    RemoveResource();
                }

                // Añadir al almacen
                gathering = false;
            }
            else
            {
                entity.GoToTarget(Home.position);

                if (currentResource != null && currentResource.GatherResource())
                {
                    TakeResource();
                    gathering = true;
                }
            }
        }
        
    }
    public override void OnExitState()
    {
        base.OnExitState();
        if (haveResource)
        {
            RemoveResource();
        }
        entity.StopAgent();
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
        resourcePiece.transform.parent = null;
        resourcePiece.GetComponent<Rigidbody>().useGravity = true;
        resourcePiece.GetComponent<SphereCollider>().enabled = true;
    }
}
