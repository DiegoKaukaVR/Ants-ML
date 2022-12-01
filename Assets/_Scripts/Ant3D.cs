using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ant3D : Agent3D
{
    protected override void Start()
    {
        base.Start();
        Initialization3D();
    }
    void Initialization3D()
    {
        Debug.Log("3D Ant");
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
        GoToTarget(FindRandomValidNavmeshPoint());
    }

    #region Movement 3D

    protected void Update()
    {

        UpdateLogic3D();


    }
    protected void FixedUpdate()
    {

        UpdateLogic3D();

    }


    public void UpdateLogic3D()
    {
        if (myNavmeshAgent.remainingDistance <= 0.2f)
        {
            GoToTarget(FindRandomValidNavmeshPoint());
        }
    }



    #endregion

    private void OnDrawGizmosSelected()
    {
        if (antType == IABase.Mode.Mode3D)
        {
            if (targetPos == Vector3.zero)
            {
                return;
            }
            Gizmos.DrawCube(targetPos, Vector3.one * 0.3f);
        }
    }
}
