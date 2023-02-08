using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : MonoBehaviour
{
    public List<TransitionBase> transitions;
    public virtual void OnEnterState()
    {

    }

    public virtual void OnExecuteState()
    {

    }

    public virtual void OnExitState()
    {

    }
}
