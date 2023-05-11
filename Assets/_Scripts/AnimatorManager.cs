using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Character entity;
    Animator animator;

    Rigidbody2D rb2D;

    private void Start()
    {
        entity = GetComponentInParent<Character>();
        animator = GetComponent<Animator>();

        if (entity.antType == Character.Mode.Pathfinding)
        {
            rb2D = entity.GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        entity.isPerformingAction = animator.GetBool("isPerformingAction");
        if (entity.antType == Character.Mode.Mode3D)
        {
            UpdateXZVelocity();
        }
        if (entity.antType == Character.Mode.Pathfinding)
        {
            UpdateXZVelocityRB();
        }
       
    }

    public virtual void UpdateXZVelocityRB()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(rb2D.velocity);
        interpolatedVelocity = Vector3.Lerp(interpolatedVelocity, localVelocity, 0.1f);
        animator.SetFloat("ZSpeed", interpolatedVelocity.z);
       
    }

    Vector3 interpolatedVelocity;
    public virtual void UpdateXZVelocity()
    {
        if (entity.antType == Character.Mode.Mode3D)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(entity.myNavmeshAgent.velocity);
            interpolatedVelocity = Vector3.Lerp(interpolatedVelocity, localVelocity, 0.1f);
            animator.SetFloat("ZSpeed", interpolatedVelocity.magnitude);
            return;
        }
    
    }


    public void ColliderAttack()
    {
        entity.damageDealer.ColliderDamage.enabled = !entity.damageDealer.ColliderDamage.enabled;
    }
}
