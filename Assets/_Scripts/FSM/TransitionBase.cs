using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionBase : MonoBehaviour
{
    bool transition;

    public virtual bool CheckTransition()
    {
        if (transition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
