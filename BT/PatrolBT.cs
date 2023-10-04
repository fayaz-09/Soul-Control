using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class PatrolBT : Node
{
    
    private Enemy m_enemy;
   
    public PatrolBT(Enemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Patrol");
        m_enemy.patrol();
        state = NodeState.RUNNING;
        return state;
    }
}

