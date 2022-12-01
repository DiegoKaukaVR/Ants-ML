using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent3D : IABase
{
  
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

}
