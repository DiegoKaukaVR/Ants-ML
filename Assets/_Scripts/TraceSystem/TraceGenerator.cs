using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceGenerator : MonoBehaviour
{
   
    [Header("Generator Configuration")]
    Ant3D ant;
    int currentStep;
    public int maxRegisterSteps = 1000;
    public float lenghtSteps = 20f;

    [Header("Trace Configuration")]
    TraceManager traceManager;
    Trace trace = new Trace();
    public class Trace
    {
        public Queue<TraceManager.Info> infoQueue;
        public Queue<Transform> pointsQueue = new Queue<Transform>();
    }
    
    public GameObject prefabSphereDebug;

    [Header("Info configuration")]
    [SerializeField] TraceManager.Info currentInfo;
  

    [SerializeField] Material whiteMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material redMaterial;

    private void Start()
    {
        ant = GetComponentInParent<Ant3D>();
        traceManager = TraceManager.instance;
    }
    private void FixedUpdate()
    {
        if (FixedFrameCount() % lenghtSteps == 0)
        {
            if (currentStep > maxRegisterSteps)
            {
                DeleteTrace();
            }
            else
            {
                currentStep++;
            }

            SetPathPoint();
        }
    }

    public static int FixedFrameCount()
    {
        return Mathf.RoundToInt(Time.fixedTime / Time.fixedDeltaTime);
    }

    void SetPathPoint()
    {
        ///DEBUG
        GameObject point = Instantiate(prefabSphereDebug, transform.position, Quaternion.identity);

        /// REGISTER POINT IN THE QUEUE 
        trace.pointsQueue.Enqueue(point.transform);

        /// CLASIFY TYPE INFO - RED WHITE OR GREEN
        point.GetComponent<MeshRenderer>().material = GetMaterial(currentInfo);
    }

    void DeleteTrace()
    {
        trace.pointsQueue.Dequeue().gameObject.SetActive(false);

        /// AVISAR AL TRACEMANAGER
        traceManager.UpdateQueue(trace.pointsQueue, ant);

    }

    public void SetCurrentInfo(int index)
    {
        switch (index)  
        {
            case 0:
                currentInfo = TraceManager.Info.white;
                break;
            case 1:
                currentInfo = TraceManager.Info.green;
                break;
            case 2:
                currentInfo = TraceManager.Info.red;
                break;
            default:
                break;
        }
    }

    Material provisionalMaterial;
    Material GetMaterial(TraceManager.Info infoType)
    {
        switch (infoType)
        {
            case TraceManager.Info.white:
                return whiteMaterial;
             
            case TraceManager.Info.green:
                return greenMaterial;
              
            case TraceManager.Info.red:
                return redMaterial;
                
            default:
                return whiteMaterial;
        }
    }




}
