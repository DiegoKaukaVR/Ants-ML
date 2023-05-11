using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TraceManager : MonoBehaviour
{
    public static TraceManager instance;

    public class Trace
    {
        public Trace(Queue<Transform> tracePath, Transform target)
        {
            tracePos = tracePath;
            Target = target;
        }
        public Queue<Transform> tracePos;
        public Info info;
        public int idColony;
        public Transform Target;
        public Character ant;
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
    Vector3 A;
    Vector3 B;
    Vector3 C;

    public Trace CheckTraceProximity(ref Character ant)
    {
        foreach (KeyValuePair<Character, Trace> element in DictionaryAllTraces)
        {
            // If ant == ant
            if (element.Key == ant)
            {
                continue;
            }

            for (int i = 0; i < element.Value.tracePos.Count; i++)
            {
                if (i+1 > element.Value.tracePos.Count-1)
                {
                    continue;
                }

                A = element.Value.tracePos.ElementAt<Transform>(i).position;
                B = element.Value.tracePos.ElementAt<Transform>(i+1).position;
                C = ant.transform.position;

                // Check position in Line
                if (LineDetection.DistanceLineSegmentPoint(A, B, C) < 0.1f)
                {  
                    // Return All Queue (Trace)
                    ant.traceTarget = element.Value.tracePos;
                    // Get Information
                    return element.Value;
                }
            }
        }

        return null;
    }

    bool idColonySet;
    public void UpdateQueue(Queue<Transform> queue, Ant3D ant, Info traceInfo)
    {
        if (!DictionaryAllTraces.ContainsKey(ant))
        {
            
               DictionaryAllTraces.Add(ant, new Trace(queue, ant.Target));
           
            
        }
        

        if (!idColonySet)
        {
            idColonySet = true;
            DictionaryAllTraces[ant].idColony = ant.colonyID;
        }

        DictionaryAllTraces[ant].tracePos = queue;
        DictionaryAllTraces[ant].info = traceInfo;
        DictionaryAllTraces[ant].Target = ant.Target;
        DictionaryAllTraces[ant].ant = ant;
    }

    public void DeleteTraceFromQueue(Ant3D ant)
    {
        if (DictionaryAllTraces.ContainsKey(ant))
        {
            DictionaryAllTraces.Remove(ant);
        }
    }
}
