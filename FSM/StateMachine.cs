using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GetInitialState();
        if(currentState != null)
        {
            currentState.Enter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(BaseStates newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseStates GetInitialState()
    {
        return null;
    }


}
