using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    public int idColony;

    [Header("Population")]
    public int Population;
    public Character queen;
    public List<Character> workers;
    public List<Character> males;
    public List<Character> princesses;






    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, Vector3.one * 0.3f);
    }
}
