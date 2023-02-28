using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TraceManager : MonoBehaviour
{
    public static TraceManager instance;

    public class Trace
    {
        public Queue<Transform> tracePos;
        public Info info;


    }
    public enum Info
    {
        white,
        green,
        red,
        none
    }

    Dictionary<Character, Trace> DictionaryAllTraces = new Dictionary<Character, Trace>();
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

    public Info CheckTraceProximity(ref Character ant)
    {
        foreach (KeyValuePair<Character, Trace> element in DictionaryAllTraces)
        {
            for (int i = 0; i < element.Value.tracePos.Count; i++)
            {
                // If ant == ant
                if (element.Key == ant)
                {
                    continue;
                }


                A = element.Value.tracePos.Peek().position;
                B = element.Value.tracePos.ElementAt<Transform>(1).position;
                C = ant.transform.position;

                // Check position in Line
                if (LineDetection.DistanceLineSegmentPoint(A, B, C) < 0.1f)
                {  
                    // Return All Queue (Trace)
                    ant.traceTarget = element.Value.tracePos;
                    // Get Information
                    return element.Value.info;
                }
            }
        }
        return Info.none;

    }
   
    public void UpdateQueue(Queue<Transform> queue, Ant3D ant)
    {
        DictionaryAllTraces[ant].tracePos = queue;
    }
}
