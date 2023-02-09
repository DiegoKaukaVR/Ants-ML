using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StateBase : MonoBehaviour
{
    [Header("State Configuration")]
    public string NameState;
    public List<Transition> transitions = new List<Transition>();
    public StateEvents stateEvents;

    [HideInInspector] public FSM fsm;
    [HideInInspector] public Ant3D entity;

    [System.Serializable]
    public class StateEvents
    {
        public UnityEvent OnEnterEvent;
        public UnityEvent OnExitEvent;
    }

    [System.Serializable]
    public class Transition
    {
        public string NameState;
        public TransitionBase transitionType;
        public bool avaible;
    }

    public void EnableTransition(int index, bool value)
    {
        transitions[index].avaible = value;
    }

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Ant3D>();
    }
    public virtual void OnEnterState()
    {
        Debug.Log(NameState + " state enter");
        stateEvents.OnEnterEvent.Invoke();
    }

    public virtual void OnExecuteState()
    {

    }

    public virtual void OnExitState()
    {
        Debug.Log(NameState + " state exit");
        stateEvents.OnExitEvent.Invoke();
    }
}
