using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
public class StrafeMageBT : Node
{
    private MageEnemy m_enemy;
    public StrafeMageBT(MageEnemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Strafe player");
        Transform target = (Transform)GetData("target");
        if (m_enemy.getCurrentStrafeTime() >= m_enemy.minStrafeTime)
        {
            m_enemy.chaseTarget(target);
            m_enemy.setStrafeFalse();
            m_enemy.canAttack = true;
        }
        else
        {
            m_enemy.strafeAroundTarget(target, MageBT.circleRange);
            m_enemy.setStrafeTrue();
        }
        state = NodeState.RUNNING;
        return state;
    }
}
