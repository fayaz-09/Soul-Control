using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class GoToPlayerBT : Node
{
    private Enemy m_enemy;

    public GoToPlayerBT(Enemy enemy)
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
