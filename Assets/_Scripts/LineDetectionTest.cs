using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetectionTest : MonoBehaviour
{
    public Transform Target;

    public Transform A;
    public Transform B;

    private void Update()
    {
        if (DistanceLineSegmentPoint(A.position, B.position, Target.position)<0.2f)
        {
            Debug.Log("InLine");
        }
      
    }
    public static float DistanceLineSegmentPoint(Vector3 start, Vector3 end, Vector3 point)
    {
        var wander = point - start;
        var span = end - start;

        // Compute how far along the line is the closest approach to our point.
        float t = Vector3.Dot(wander, span) / span.sqrMagnitude;

        // Restrict this point to within the line segment from start to end.
        t = Mathf.Clamp01(t);

        Vector3 nearest = start + t * span;
        return (nearest - point).magnitude;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(A.position, B.position);
    }
}
