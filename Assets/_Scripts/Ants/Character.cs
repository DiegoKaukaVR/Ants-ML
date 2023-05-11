using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public int colonyID;
    public bool queen;

    public Transform Target;

    public FSM fsm;
    [HideInInspector] public Animator animator;
    [HideInInspector] public AnimatorManager animatorManager;
    [HideInInspector] public NavMeshAgent myNavmeshAgent;
    [HideInInspector] public DamageDealer damageDealer;
    [HideInInspector] public UIManagerUnit uiManagerUnit;
    Collider coll;

    public bool isPerformingAction;

    public Queue<Transform> traceTarget;


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
        Pathfinding
    }

    public Mode antType;

    protected virtual void Start()
    {
        myNavmeshAgent = GetComponent<NavMeshAgent>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
        animator = animatorManager.GetComponent<Animator>();
        damageDealer = GetComponentInChildren<DamageDealer>();
        uiManagerUnit = GetComponentInChildren<UIManagerUnit>();
        fsm = GetComponentInChildren<FSM>();
        currentHp = maxHp;
        coll = GetComponent<Collider>();
    }

    public float rotationSpeed = 3f;
    public void SlerpRotationVelocity(float angle)
    {
        if (isPerformingAction)
        {
            return;
        }
        Vector3 dirToTarget = myNavmeshAgent.velocity;
        dirToTarget.y = 0;
        if (Mathf.Abs(angle) < 30)
        {
            if (Mathf.Abs(angle) > 15)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * 3f * rotationSpeed);
                if (angle > -180 && angle < 0)
                {
                    animator.SetFloat("XSpeed", -8 * (angle / -179));
                }

                else if (angle > 0)
                {
                    animator.SetFloat("XSpeed", +8 * (angle / 179));
                }

            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * 5f * rotationSpeed);
                //if (angle < 5)
                //{
                //    if (myNavMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
                //    {
                //        velocityClamped = myNavMeshAgent.velocity;
                //        velocityClamped.y = 0;
                //        transform.rotation = Quaternion.LookRotation(velocityClamped.normalized);
                //    }
                //    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(CheckDirectionTarget()), Time.deltaTime * 10f);

                //    //Debug.Log("Hard Lock");
                //}

                return;
            }

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * 2.3f);

            if (angle > -180 && angle < 0)
            {
                animator.SetFloat("XSpeed", -4 * (angle / -179));
            }

            else if (angle > 0)
            {
                animator.SetFloat("XSpeed", +4 * (angle / 179));
            }
        }
    }
    public void SlerpRotationTarget(float angle)
    {
        if (isPerformingAction)
        {
            return;
        }
        Vector3 dirToTarget = CheckDirectionTarget();
        dirToTarget.y = 0;
        if (Mathf.Abs(angle) < 30)
        {
            if (Mathf.Abs(angle) > 15)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * 3f * rotationSpeed);
                if (angle > -180 && angle < 0)
                {
                    animator.SetFloat("XSpeed", -8 * (angle / -179));
                }

                else if (angle > 0)
                {
                    animator.SetFloat("XSpeed", +8 * (angle / 179));
                }

            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * 5f * rotationSpeed);
                //if (angle < 5)
                //{
                //    if (myNavMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
                //    {
                //        velocityClamped = myNavMeshAgent.velocity;
                //        velocityClamped.y = 0;
                //        transform.rotation = Quaternion.LookRotation(velocityClamped.normalized);
                //    }
                //    //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(CheckDirectionTarget()), Time.deltaTime * 10f);

                //    //Debug.Log("Hard Lock");
                //}

                return;
            }

        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToTarget), Time.deltaTime * 2.3f);

            if (angle > -180 && angle < 0)
            {
                animator.SetFloat("XSpeed", -4 * (angle / -179));
            }

            else if (angle > 0)
            {
                animator.SetFloat("XSpeed", +4 * (angle / 179));
            }
        }
    }

    public void ReceiveDamage(int damage, Vector3 sourceDamage)
    {
        currentHp -= damage;
        uiManagerUnit.HPBar.SetHPBar(currentHp, maxHp);
        
        if (currentHp <= 0)
        {
            Death();
        }
        else
        {
            animator.SetTrigger("Hit");
            DirectionDamageCalculation(CheckDirectionTarget(sourceDamage));
        }
    }

    /// <summary>
    /// Calculates direction from source damage and send animator XZ directional parameters
    /// </summary>
    void DirectionDamageCalculation(Vector3 dir)
    {
        float angle = Vector3.SignedAngle(transform.forward, -dir, Vector3.up);
        float normalizedAngle = 0;

        #region
        if (angle > 0 && angle < 45)
        {
            normalizedAngle = Mathf.Abs(angle) / 90;
            animator.SetFloat("XDamage", 0);
            animator.SetFloat("ZDamage", 1);
            return;
        }
        if (angle < 0 && angle > -45)
        {
            normalizedAngle = angle / 90;
            animator.SetFloat("XDamage", 0);
            animator.SetFloat("ZDamage", 1);
            return;
        }

        if (angle >= 45 && angle < 90)
        {
            normalizedAngle = Mathf.Abs(angle - 90) / 90;
            animator.SetFloat("XDamage", 1);
            animator.SetFloat("ZDamage", 0);
            return;
        }
        if (angle <= -45 && angle > -90)
        {
            normalizedAngle = Mathf.Abs(angle + 90) / 90;
            animator.SetFloat("XDamage", -1);
            animator.SetFloat("ZDamage", 0);
            return;
        }

        if (angle >= 90 && angle < 120)
        {
            normalizedAngle = Mathf.Abs(angle - 90) / 90;
            animator.SetFloat("XDamage", +1);
            animator.SetFloat("ZDamage", 0);
            return;
        }
        if (angle <= 90 && angle > -120)
        {
            normalizedAngle = Mathf.Abs(angle + 90) / 90;
            animator.SetFloat("XDamage", -1);
            animator.SetFloat("ZDamage", 0);
            return;
        }
        if (angle > 120 && angle <= 180)
        {
            normalizedAngle = Mathf.Abs(angle - 90) / 90;
            animator.SetFloat("XDamage", 0);
            animator.SetFloat("ZDamage", -1);
            return;
        }
        if (angle < -120 && angle >= -180)
        {
            normalizedAngle = Mathf.Abs(angle + 90) / 90;
            animator.SetFloat("XDamage", 0);
            animator.SetFloat("ZDamage", -1);
            return;
        }
        #endregion
    }

    public bool dead;
    public void Death()
    {
        fsm.ExitFSM();
        animator.SetTrigger("Death");
        myNavmeshAgent.enabled = false;
        coll.enabled = false;
        dead = true;

    }
    public float CheckDistanceTarget()
    {
        return Vector3.Distance(Target.position, transform.position);
    }
    public float CheckDistanceTarget(Vector3 pos)
    {
        return Vector3.Distance(pos, transform.position);
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
