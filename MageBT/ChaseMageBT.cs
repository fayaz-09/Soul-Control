using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class ChaseMageBT : Node
{
    private MageEnemy m_enemy;

    public ChaseMageBT(MageEnemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    {

        Debug.Log("Chase player");
        Transform target = (Transform)GetData("target");
        m_enemy.setStrafeFalse();
        m_enemy.chaseTarget(target);
        state = NodeState.RUNNING;
        return state;
    }
}
