using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceManager : MonoBehaviour
{
    public static TraceManager instance;
    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    /// <summary>
    /// List with all traces 
    /// </summary>
    List<Queue<Vector3>> AllAntsTraces;

   
    public void UpdateQueue(Queue<Vector3> queue, Ant3D ant)
    {

    }
}
