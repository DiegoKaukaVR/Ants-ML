using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ant : MonoBehaviour
{
    public int colonyID;

    public bool queen;

    public Transform Target;

    [HideInInspector] public AnimatorManager animatorManager;
    [HideInInspector] public NavMeshAgent myNavmeshAgent;

    #region Stats System
    [Header("Stats")]
    public int currentHp;
    public int maxHp = 10;
    public int damage = 1;
    public int strenght = 5;
 

    public statsBase Stats;

    [System.Serializable]
    public struct statsBase
    {
        public int baseHP;
        public int baseDamage;
        public int baseSpeed;
    }

    #endregion

    public enum Mode
    {
        Mode2D,
        Mode3D,
    }

    public Mode antType;

    protected virtual void Start()
    {
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
    }
}
