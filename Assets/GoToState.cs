using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToState : StateBase
{
    public Transform target;


    public override void OnEnterState()
    {
        entity.GoToTarget(target.position);
    }
}
