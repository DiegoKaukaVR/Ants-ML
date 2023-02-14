using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    Ant entity;
    Animator animator;

    private void Start()
    {
        entity = GetComponentInParent<Ant>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        entity.isPerformingAction = animator.GetBool("isPerformingAction");
        if (entity.antType == Ant.Mode.Mode3D)
        {
            UpdateXZVelocity();
        }
       
    }

    Vector3 interpolatedVelocity;
    public virtual void UpdateXZVelocity()
    {
        if (entity.antType == Ant.Mode.Mode3D)
        {
            Vector3 localVelocity = transform.InverseTransformDirection(entity.myNavmeshAgent.velocity);
            interpolatedVelocity = Vector3.Lerp(interpolatedVelocity, localVelocity, 0.1f);
            animator.SetFloat("ZSpeed", interpolatedVelocity.z);
            return;
        }
    
    }
}
