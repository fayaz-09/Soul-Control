using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatesHFSM 
{
    public string name;
    protected HierarchicalStateMachine stateMachine;

    public BaseStatesHFSM(string name, HierarchicalStateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
