using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public int colonyID;
    public bool queen;

    public Transform Target;

    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimatorManager animatorManager;
    [HideInInspector] public NavMeshAgent myNavmeshAgent;
    [HideInInspector] public DamageDealer damageDealer;
    [HideInInspector] public UIManagerUnit uiManagerUnit;

    public bool isPerformingAction;


    #region Stats System
    [Header("Stats")]
    public int currentHp;
    public int maxHp = 10;
    public int damage = 1;
    public int strenght = 5;
 

    public statsBase Stats;

    [System.Serializable]
    public class statsBase
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
        animator = animatorManager.GetComponent<Animator>();
        damageDealer = GetComponentInChildren<DamageDealer>();
        uiManagerUnit = GetComponentInChildren<UIManagerUnit>();
        currentHp = maxHp;
    }

    public void ReceiveDamage(int damage)
    {
        currentHp -= damage;
        uiManagerUnit.HPBar.SetHPBar(currentHp, maxHp);
        
        if (currentHp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        animator.SetTrigger("Death");
     
       
    }
    public float CheckDistanceTarget()
    {
        return Vector3.Distance(Target.position, transform.position);
    }
    public Vector3 CheckDirectionTarget()
    {
        return Target.position - transform.position;
    }

    public Vector3 CheckDirectionTarget(Vector3 pos)
    {
        return pos - transform.position;
    }
}
