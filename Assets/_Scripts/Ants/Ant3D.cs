using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ant3D : Character
{
    [Header("Ant 3D")]
    [SerializeField] protected Vector3 targetPos;
    public TraceGenerator traceGenerator;


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

        traceGenerator = GetComponentInChildren<TraceGenerator>();

        GameManager.instance.allAnts.Add(this);
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
        }
        else
        {
            //GoToTarget(Target.position); 
        }
      
    }



    #endregion



    public void GoToTarget(Vector3 pos)
    {
        Debug.DrawLine(transform.position, pos, Color.yellow);
        targetPos = pos;
        myNavmeshAgent.SetDestination(pos);
    }
    public void StopAgent()
    {
        myNavmeshAgent.velocity = Vector3.zero;
        myNavmeshAgent.ResetPath();
    }

    public Vector3 FindRandomValidNavmeshPoint()
    {
        Vector3 finalPoint = new Vector3();

        Vector3 randomPoint = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomPoint, out navMeshHit, 0.2f, NavMesh.AllAreas))
        {
            finalPoint = navMeshHit.position;
        }

        return finalPoint;
    }

    public override void Death()
    {
        base.Death();
        TraceManager.instance.DeleteTraceFromQueue(this);
    }
    private void OnDrawGizmosSelected()
    {
        if (antType == Character.Mode.Mode3D)
        {
            if (targetPos == Vector3.zero)
            {
                return;
            }
            Gizmos.DrawCube(targetPos, Vector3.one * 0.3f);
        }
    }

    
}
