using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class MageAttackBT : Node
{
    private MageEnemy m_enemy;
    public MageAttackBT(MageEnemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Range Attack player");
        Transform target = (Transform)GetData("target");
        m_enemy.rangeAttack(target);
        state = NodeState.RUNNING;
        return state;
    }
}
