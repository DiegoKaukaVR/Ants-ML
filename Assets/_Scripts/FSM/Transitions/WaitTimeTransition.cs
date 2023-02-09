using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTimeTransition : TransitionBase
{
    [SerializeField] float currentTimer;
    [SerializeField] float maxTime;
    public override bool CheckTransition()
    {
        if (currentTimer>maxTime)
        {
            currentTimer = 0;
            return true;
        }
        else
        {
            currentTimer += Time.deltaTime;
            return false;
        }
    }
}
