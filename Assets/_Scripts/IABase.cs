using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABase : MonoBehaviour
{
    [HideInInspector] public AnimatorManager animatorManager;
    [HideInInspector] public NavMeshAgent myNavmeshAgent;

    public enum Mode
    {
        Mode2D,
        Mode3D,
    }

    public Mode antType;

    protected virtual void Start()
    {
       
    }
}
