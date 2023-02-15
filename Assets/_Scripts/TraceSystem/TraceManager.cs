using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TraceManager : MonoBehaviour
{
    public static TraceManager instance;

    public enum Info
    {
        white,
        green,
        red
    }

    Dictionary<Character, Queue<Transform>> DictionaryAllTraces = new Dictionary<Character, Queue<Transform>>();
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
    /// Returns TRACE
    /// </summary>
    /// 
    Vector3 A;
    Vector3 B;
    Vector3 C;

    public bool CheckTraceProximity(Character ant)
    {
        foreach (KeyValuePair<Character, Queue<Transform>> element in DictionaryAllTraces)
        {
            for (int i = 0; i < element.Value.Count; i++)
            {
                // If ant == ant
                if (element.Key == ant)
                {
                    continue;
                }


                A = element.Value.Peek().position;
                B = element.Value.ElementAt<Transform>(1).position;
                C = ant.transform.position;

                // Check position in Line
                if (LineDetection.DistanceLineSegmentPoint(A, B, C) < 0.1f)
                {
                    return true;

                }

                // Get Information

                // Return All Queue (Trace)

               
            }
          

        }
        
        return false;
    }
   
    public void UpdateQueue(Queue<Transform> queue, Ant3D ant)
    {
        DictionaryAllTraces[ant] = queue;
    }
}
