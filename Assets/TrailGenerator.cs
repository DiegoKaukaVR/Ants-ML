using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailGenerator : MonoBehaviour
{
    [Header("Trace System")]

    int currentStep;
    public int maxRegisterSteps = 1000;
    public float lenghtSteps = 120f;

    public Queue<Transform> pointsQueue = new Queue<Transform>();

    public GameObject prefabSphereDebug;

    [SerializeField] Material whiteMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material redMaterial;

    
    private void Update()
    {
        if (Time.frameCount % lenghtSteps == 0)
        {
            if (currentStep > maxRegisterSteps)
            {
                pointsQueue.Dequeue().gameObject.SetActive(false);
            }
            else
            {
                currentStep++;
            }

            SetPathPoint();
        }
    }

    void SetPathPoint()
    {
        /// REGISTER POINT IN THE QUEUE 
        
        ///DEBUG
        GameObject point = Instantiate(prefabSphereDebug, transform.position, Quaternion.identity);

        pointsQueue.Enqueue(point.transform);

        /// CLASIFY TYPE INFO - RED WHITE OR GREEN
    }




}
