using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;

public class StrafeBT : Node
{
    private Enemy m_enemy;
    public StrafeBT(Enemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Strafe player");
        Transform target = (Transform)GetData("target");
        if(m_enemy.getCurrentStrafeTime() >= m_enemy.minStrafeTime)
        {
            m_enemy.chaseTarget(target);
            m_enemy.setStrafeFalse();
            m_enemy.canAttack = true;
        }
        else
        {
            m_enemy.strafeAroundTarget(target, EnemyBT.circleRange);
            m_enemy.setStrafeTrue();
        }
        state = NodeState.RUNNING;
        return state;
    }
}
