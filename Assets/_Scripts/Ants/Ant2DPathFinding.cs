using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class Ant2DPathFinding : Character
{
    public Vector2 targetPos2D;
    Rigidbody2D rb2D;
    public float detectionGround2D = 0.5f;
    public LayerMask GroundLayer;
    Vector2 frontVector;

    public Dir initialDir;
    public Dir currentDir;

    public Grid grid;
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
   
    protected void FixedUpdate()
    {
        GoPath();
    }

    [SerializeField] Transform antTransform;
    void GoPath()
    {
        if (path.Count == 0)
        {
            return;
        }
        Debug.DrawLine(transform.position, path[indexPath], Color.blue);
        if (Vector2.Distance(transform.position, path[indexPath])<0.25f)
        {
            
            Debug.Log("NEXT POINT");
            if (indexPath +1 < path.Count)
            {
                indexPath++;
            }
          
           
        }
        Vector2 dir = path[indexPath] - new Vector2(transform.position.x, transform.position.y);

        float angle = Vector2.SignedAngle(transform.up, dir);
        Vector2 direction = Vector2.up.Rotate(angle);
        transform.Rotate(0, 0, angle);

        dir.Normalize();
        Debug.DrawRay(transform.position, dir);
        rb2D.velocity = dir * speed*Time.fixedDeltaTime;
    }



    List<Vector2> path = new List<Vector2>();
    int indexPath;
    public void NewPathFinding2D()
    {
        path.Clear();
        indexPath = 0;
        if (grid.path == null)
        {
            return;
        }
        
        for (int i = 0; i < grid.path.Count; i++)
        {
            path.Add(new Vector2(grid.path[i].worldPosition.x, grid.path[i].worldPosition.y));
        }
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
