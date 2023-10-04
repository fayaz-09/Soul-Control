using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class EnemyAttackBT : Node
{
    private Enemy m_enemy;
    public EnemyAttackBT(Enemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    { 
        Debug.Log("Attack player");
        Transform target = (Transform)GetData("target");
        m_enemy.attackTarget(target);
        state = NodeState.RUNNING;
        return state;
    }
}
