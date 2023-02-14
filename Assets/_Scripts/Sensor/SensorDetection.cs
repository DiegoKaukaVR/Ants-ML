using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDetection : SensorBase
{
    public float radius;
    public LayerMask targetLayer;
    Collider[] targetAround;

    public override bool UpdateSensor()
    {
        targetAround = Physics.OverlapSphere(transform.position, radius, targetLayer);

        if (targetAround.Length>0)
        {
            entity.Target = targetAround[0].transform.root;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
