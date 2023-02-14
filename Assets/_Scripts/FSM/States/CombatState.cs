using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : StateBase
{
    public float attackDistance = 1f;
    public float cooldownAttack = 2f;
    bool canAttack = true;

    protected override void Awake()
    {
        base.Awake();
        canAttack = true;
    }
    public override void OnExecuteState()
    {
        base.OnExecuteState();

        if (entity.isPerformingAction)
        {
            return;
        }

        entity.GoToTarget(entity.Target.position);


        if (canAttack)
        {
            if (Vector3.Distance(transform.position, entity.Target.position) < attackDistance)
            {
                Attack();
            }
        }
       
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
