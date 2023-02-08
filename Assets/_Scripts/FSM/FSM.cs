using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finite State Machine Design
/// </summary>
public class FSM : MonoBehaviour
{
    public List<StateBase> listStates;
    StateBase currentState;

    private void Awake()
    {
        ///HOW TO SET STATES
        ///HOW TO CALL STATES FROM OUTSIDE: FSM.CHANGESTATE(???)
        ///STRUCTURE? DICTIONARY BY STRING?...
    }
    private void Update()
    {
        if (currentState!=null)
        {
            currentState.OnExecuteState();
        }
    }
    public void ChangeState(StateBase newState)
    {
        currentState.OnExitState();
        currentState = newState;
        currentState.OnEnterState();
    }

    public void CheckTransitions()
    {
        for (int i = 0; i < currentState.transitions.Count; i++)
        {
            if (currentState.transitions[i].CheckTransition())
            {
                ChangeState(currentState.transitions[i].transitionState);
            }
        }
    }
}
