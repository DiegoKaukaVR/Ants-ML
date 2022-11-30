using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Tools;



public class Ant : IABase
{

    protected override void Start()
    {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (antType == Mode.Mode3D)
        {
            UpdateLogic3D();

        }
        else
        {
            Update2D();
        }

    }

    private void FixedUpdate()
    {
        if (antType == Mode.Mode3D)
        {
            UpdateLogic3D();

        }
        else
        {
            UpdatePyhsics2D();
        }
    }
    #region Movement 2D

    Rigidbody2D rb2D;
    public float detectionGround2D = 0.5f;
    public LayerMask GroundLayer;
    public void Update2D()
    {
        RaycastHit[] results1;
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, -transform.up, detectionGround2D, 6); // Ground Layer
       
        if (raycastHit.collider != null && DetectLayer.LayerInLayerMask(raycastHit.collider.gameObject.layer, GroundLayer))
        {
            Debug.DrawRay(transform.position, -transform.up, Color.green);
            Debug.DrawRay(raycastHit.point, raycastHit.normal, Color.red);
         

        }


    }
    
    public void UpdatePyhsics2D()
    {
      
    }



    #endregion





    #region Movement 3D

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
        else
        {
            if (targetPos2D != Vector2.zero)
            {
                Gizmos.DrawCube(targetPos2D, Vector2.one);
            }
          
          



        }
    
    }


}
