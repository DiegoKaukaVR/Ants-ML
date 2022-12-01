using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class Ant2D : Ant
{
    public Vector2 targetPos2D;
    Rigidbody2D rb2D;
    public float detectionGround2D = 0.5f;
    public LayerMask GroundLayer;
    Vector2 frontVector;

    public Dir initialDir;
    public Dir currentDir;
    public enum Dir
    {
        right,
        left
    }
    protected override void Start()
    {
        base.Start();
        Initialization2D();

        if (initialDir == Dir.left)
        {
            transform.Rotate(0, -180, 0);
        }


        currentDir = initialDir;
        distBtwRays = (maxDist / 2f) / numberOfRays;
    }

    void Initialization2D()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    protected void Update()
    {
         Update2D();
    }
    protected void FixedUpdate()
    {
         UpdatePyhsics2D();  
    }

    [SerializeField] Transform detectionGround;
    [SerializeField] int numberOfRays = 5;
    float distBtwRays;
    [SerializeField] float maxDist = 0.3f;

    public void Update2D()
    {
        #region Advanced Method

        float dist = 0;
        for (int i = 0; i < numberOfRays; i++)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(detectionGround.position, -transform.up, detectionGround2D, GroundLayer); // Ground Layer
            Vector3 newPos = detectionGround.position + transform.right * dist;


            if (raycastHit.collider != null && DetectLayer.LayerInLayerMask(raycastHit.collider.gameObject.layer, GroundLayer))
            {
                Debug.DrawRay(newPos, -transform.up * detectionGround2D, Color.green);
                Debug.DrawRay(transform.position, raycastHit.normal, Color.red);
                Vector2 normal = raycastHit.normal;
                RotateAnt(normal);

                Debug.DrawRay(transform.position, frontVector, Color.yellow);
                frontVector = Rotate(ref normal, -90);
                MovementAnt(frontVector);
                break;
            }

            RaycastHit2D circleCast = Physics2D.CircleCast(detectionGround.position, 0.2f, -transform.up, detectionGround2D, GroundLayer);
          
            if (circleCast.collider != null && DetectLayer.LayerInLayerMask(circleCast.collider.gameObject.layer, GroundLayer))
            {
                Debug.DrawRay(newPos, -transform.up * detectionGround2D, Color.green);
                Debug.DrawRay(transform.position, circleCast.normal, Color.red);
                Vector2 normal = circleCast.normal;
                RotateAnt(normal);

                Debug.DrawRay(transform.position, frontVector, Color.yellow);
                frontVector = Rotate(ref normal, -90);
                MovementAnt(frontVector);
                break;
            }

            dist += distBtwRays;
            
        }

        #endregion


    }


    void RotateAnt(Vector2 normalVector)
    {
        Vector2 v1 = transform.up;
        Vector2 v2 = normalVector;

        float angle = Vector2.SignedAngle(v1, v2);

        //SLERP ROTATION


        if (currentDir == Dir.right)
        {
            transform.Rotate(-angle, 0, 0);
        }
        else
        {
            transform.Rotate(angle, 0, 0);
        }
        
    }

    [SerializeField] float speed = 0.5f;
    void MovementAnt(Vector3 frontDir)
    {
        if (currentDir == Dir.right)
        {
            transform.position += frontDir.normalized * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= frontDir.normalized * speed * Time.deltaTime;
        }
        
    }

    public static Vector2 Rotate(ref Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }
    public void UpdatePyhsics2D()
    {

    }

}
