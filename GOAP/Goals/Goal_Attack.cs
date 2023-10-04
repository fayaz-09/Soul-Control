using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Attack : Goal_Base
{
    [SerializeField] int attackPriority = 80;
    [SerializeField] float distToattack = 4f;
    [SerializeField] public Transform m_playerTransform;
    int currentPriority = 0;

    public override void onTickGoal()
    {
        currentPriority = 0;
        if (!m_enemy.checkWithinRange(distToattack, m_playerMask))
        {
            m_playerTransform = null;
            return;
        }

        if (m_playerTransform != null)
        {
            float dist = m_enemy.getDistance(m_playerTransform);
            if (dist <= distToattack && m_enemy.canAttack)
            {
                currentPriority = attackPriority;
                return;
            }
            else
            {
                currentPriority = 0;
                m_playerTransform = null;
                return;
            }
        }

        if (m_enemy.checkWithinRange(distToattack, m_playerMask))
        {
            m_playerTransform = m_enemy.getTargetInRange(distToattack, m_playerMask);
            currentPriority = attackPriority;
            return;
        }
    }

    public override void onGoalDeactivated()
    {
        base.onGoalDeactivated();
        m_playerTransform = null;
    }

    public override float onCaclculatePriority()
    {
        return currentPriority;
    }

    public override bool canRun()
    {
        if (!m_enemy.checkWithinRange(distToattack, m_playerMask))
        {
            return false;
        }

        if (m_playerTransform != null)
        {
            float dist = m_enemy.getDistance(m_playerTransform);
            if (dist <= distToattack && m_enemy.canAttack)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (m_enemy.checkWithinRange(distToattack, m_playerMask))
        {
            return true;
        }
        return false;
    }
}
