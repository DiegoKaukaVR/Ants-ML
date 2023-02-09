using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : StateBase
{
    [Header("InitialState")]
    public StateBase firstState;

    protected override void Awake()
    {
        base.Awake();
        NameState = "InitialState";
    }

    public override void OnEnterState()
    {
        fsm.ChangeState(firstState.NameState);
    }
}
