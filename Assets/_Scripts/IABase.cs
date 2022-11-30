using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent myNavmeshAgent;
    [HideInInspector] public AnimatorManager animatorManager;

    protected Vector3 targetPos;
    public Vector2 targetPos2D;
 

    public enum Mode
    {
        Mode2D,
        Mode3D,
    }

    public Mode antType;
    protected virtual void Start()
    {
        switch (antType)
        {
            case Mode.Mode2D:
                Initialization2D();
                break;
            case Mode.Mode3D:
                Initialization3D();
                break;
            default:
                break;
        }

    }

    void Initialization2D()
    {

    }

    void Initialization3D()
    {
        Debug.Log("3D Ant");
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
        GoToTarget(FindRandomValidNavmeshPoint());
    }

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
