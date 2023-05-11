using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : StateBase
{
    public float attackDistance = 1f;
    public float minAngleAttack = 45f;
    public float cooldownAttack = 2f;
    bool canAttack = true;

    public float stoppDist = 1.45f;

    protected override void Awake()
    {
        base.Awake();
        canAttack = true;
    }


    public override void OnEnterState()
    {
        base.OnEnterState();
        entity.GoToTarget(entity.Target.position);
        Debug.Log("s2");
    }

    public override void OnFixedExecuteState()
    {
        if (entity.Target.GetComponent<Character>().dead)
        {
            entity.fsm.ChangeState("Explore");
        }
    }
    public override void OnExecuteState()
    {
        base.OnExecuteState();
       
        if (entity.isPerformingAction)
        {
            return;
        }
        float angleTarget = Vector3.Angle(entity.transform.forward, entity.CheckDirectionTarget()); 
        float dist = entity.CheckDistanceTarget();

        if (dist < stoppDist)
        {
            entity.StopAgent();
        }
        else
        {
            entity.GoToTarget(entity.Target.position);
        }



        ///rotation to target
        if (dist<3)
        {
            entity.SlerpRotationTarget(0);
        }
        else
        {
            entity.SlerpRotationVelocity(0);
        }

        if (CanAttack(angleTarget))
        {
            if (Vector3.Distance(transform.position, entity.Target.position) < attackDistance)
            {
                Attack();
            }
        }
       
    }
    bool CanAttack(float angle)
    {
       

        Debug.DrawRay(transform.position, entity.CheckDirectionTarget().normalized, Color.red);
        Debug.DrawRay(transform.position, transform.forward, Color.green);

        if (entity.Target.TryGetComponent<Character>(out Character ant))
        {
            if (ant.dead)
            {
                return false;
            }
        }
        if (angle > minAngleAttack)
        {
            return false;
        }

        if (!canAttack)
        {
            return false;
        }

        return true;
    }

    void Attack()
    {
        entity.isPerformingAction = true;
        entity.StopAgent();
        entity.animator.SetTrigger("Attack");
        Invoke("ResetAttackCooldown", cooldownAttack);
    }

    void ResetAttackCooldown()
    {
        canAttack = true;
    }
}
