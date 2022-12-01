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

    protected override void Start()
    {
        base.Start();
        Initialization2D();
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

    public void Update2D()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(detectionGround.position, -transform.up, detectionGround2D, GroundLayer); // Ground Layer
        Debug.DrawRay(detectionGround.position, -transform.up * detectionGround2D, Color.green);
        if (raycastHit.collider != null && DetectLayer.LayerInLayerMask(raycastHit.collider.gameObject.layer, GroundLayer))
        {
           
            Debug.DrawRay(transform.position, raycastHit.normal, Color.red);
            Vector2 normal = raycastHit.normal;
            RotateAnt(normal);

            Debug.DrawRay(transform.position, frontVector, Color.yellow);
            frontVector = Rotate(ref normal, -90);
            MovementAnt(frontVector);

        }


    }


    void RotateAnt(Vector2 normalVector)
    {
        Vector2 v1 = transform.up;
        Vector2 v2 = normalVector;

        float angle = Vector2.SignedAngle(v1, v2);

        transform.Rotate(-angle, 0, 0);
    }

    [SerializeField] float speed = 0.5f;
    void MovementAnt(Vector3 frontDir)
    {
        transform.position += frontDir.normalized * speed * Time.deltaTime;
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
