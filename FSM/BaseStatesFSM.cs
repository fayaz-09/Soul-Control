using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStates 
{
    public string name;
    protected StateMachine stateMachine;

    public BaseStates(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
