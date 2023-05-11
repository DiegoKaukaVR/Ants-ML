using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finite State Machine Design
/// </summary>
public class FSM : MonoBehaviour
{
    public Dictionary<string, StateBase> DictionaryStates = new Dictionary<string, StateBase>();

    [SerializeField] StateBase currentState;
    [SerializeField] StateBase previousState;
    [SerializeField] float timeInState;

    private void Awake()
    {
        InitializeFSM();
    }

    void InitializeFSM()
    {
        SetStates();
        currentState = DictionaryStates["InitialState"];
        currentState.OnEnterState();
    }

    public void ExitFSM()
    {
        currentState.OnExitState();
        this.enabled = false;
    }
    
    void SetStates()
    {
        foreach (StateBase state in GetComponentsInChildren<StateBase>())
        {
            if (DictionaryStates.ContainsKey(state.NameState))
            {
                continue;
            }
            DictionaryStates.Add(state.NameState, state);
            state.fsm = this;
        }
    }

    private void Update()
    {
        if (currentState!=null)
        {
            timeInState += Time.deltaTime;
            currentState.OnExecuteState();

            CheckTransitions();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.OnFixedExecuteState();
        }
    }
    public void ChangeState(string nameState)
    {
        StateBase newState;
        newState = DictionaryStates[nameState];
       
        currentState.OnExitState();
        previousState = currentState;
        timeInState = 0;

        currentState = newState;
     
        currentState.OnEnterState();
        currentState.OnEnterStateTransition();
    }
    public void ChangeState(StateBase newState)
    {

        currentState.OnExitState();
        previousState = currentState;
        timeInState = 0;

        currentState = newState;

        currentState.OnEnterState();
        currentState.OnEnterStateTransition();
    }

    public void CheckTransitions()
    {
        for (int i = 0; i < currentState.transitions.Count; i++)
        {
            if (currentState.transitions[i].avaible == false)
            {
                continue;
            }
            if (currentState.transitions[i].whenFalse)
            {
                for (int z = 0; z < currentState.transitions[i].transitionType.Count; z++)
                {
                    if (!currentState.transitions[i].transitionType[z].UpdateSensor())
                    {
                        ChangeState(currentState.transitions[i].NameState);
                        return;
                    }
                }
               
            }
            else
            {
                for (int y = 0; y < currentState.transitions[i].transitionType.Count; y++)
                {
                    if (currentState.transitions[i].transitionType[y].UpdateSensor())
                    {
                        ChangeState(currentState.transitions[i].NameState);
                        return;
                    }
                }
               
            }
            
        }
    }
}
