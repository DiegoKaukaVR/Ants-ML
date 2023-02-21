using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorDetection : SensorBase
{
    public float radius;
    public LayerMask targetLayer;
    Collider[] targetsAround;

    float nearestDist;
    float dist;
    int indexLessDist;
    public override bool UpdateSensor()
    {
        targetsAround = Physics.OverlapSphere(transform.position, radius, targetLayer);

        if (targetsAround.Length>0)
        {
            entity.Target = CalculateNearestCollider().root;
            return true;
        }
        else
        {
            return false;
        }
    }

    Transform CalculateNearestCollider()
    {
        dist = 0;
        indexLessDist = 0;
        for (int i = 0; i < targetsAround.Length; i++)
        {
            dist = entity.CheckDistanceTarget(targetsAround[i].transform.position);
            if (nearestDist == 0)
            {
                nearestDist = dist;
            }

            if (dist<nearestDist)
            {
                nearestDist = dist;
                indexLessDist = i;
            }
        }

        return targetsAround[indexLessDist].transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
