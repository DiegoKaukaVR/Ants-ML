using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ant3D : Ant
{
    protected override void Start()
    {
        base.Start();
        Initialization3D();
    }
    void Initialization3D()
    {
        Debug.Log("3D Ant");

        if (queen)
        {
            return;
        }
        if (colonyID == 0)
        {
            GoToTarget(FindRandomValidNavmeshPoint());
        }
        else
        {
            GoToTarget(Target.position);
        }
        
    }

    #region Movement 3D

    protected void Update()
    {
        if (queen)
        {
            return;
        }
        UpdateLogic3D();


    }
    protected void FixedUpdate()
    {
        if (queen)
        {
            return;
        }
        UpdateLogic3D();

    }


    public void UpdateLogic3D()
    {
        if (colonyID == 0)
        {
            if (myNavmeshAgent.remainingDistance <= 0.2f)
            {
                GoToTarget(FindRandomValidNavmeshPoint());
            }
        }
        else
        {
           
                GoToTarget(Target.position);
          
        }
      
    }



    #endregion

    protected Vector3 targetPos;

    public void GoToTarget(Vector3 pos)
    {
        Debug.DrawLine(transform.position, pos, Color.yellow);
        targetPos = pos;
        myNavmeshAgent.SetDestination(pos);
    }

    public Vector3 FindRandomValidNavmeshPoint()
    {
        Vector3 finalPoint = new Vector3();

        Vector3 randomPoint = new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 0.2f, NavMesh.AllAreas))
        {
            finalPoint = navMeshHit.position;
        }

        return finalPoint;
    }

    private void OnDrawGizmosSelected()
    {
        if (antType == Ant.Mode.Mode3D)
        {
            if (targetPos == Vector3.zero)
            {
                return;
            }
            Gizmos.DrawCube(targetPos, Vector3.one * 0.3f);
        }
    }
}
