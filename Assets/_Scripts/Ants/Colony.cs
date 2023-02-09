using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    public int idColony;

    [Header("Population")]
    public int Population;
    public Ant queen;
    public List<Ant> workers;
    public List<Ant> males;
    public List<Ant> princesses;






    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);
    }
}
