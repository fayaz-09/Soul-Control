using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Strafe : Goal_Base
{
    [SerializeField] int strafePriority = 70;
    [SerializeField] float distToStrafe = 10f;
    [SerializeField] public Transform m_playerTransform;
    int currentPriority = 0;

    public override void onTickGoal()
    {
        currentPriority = 0;

        if (!m_enemy.checkWithinRange(distToStrafe, m_playerMask))
        {
            m_playerTransform = null;
            return;
        }

        if (m_playerTransform != null)
        {
            float dist = m_enemy.getDistance(m_playerTransform);
            if (dist <= distToStrafe)
            {
                currentPriority = strafePriority;
                return;
            }
            else if (dist > distToStrafe)
            {
                currentPriority = 0;
                m_playerTransform = null;
                return;
            }
        }
        if (m_enemy.checkWithinRange(distToStrafe, m_playerMask))
        {
            m_playerTransform = m_enemy.getTargetInRange(distToStrafe, m_playerMask);
            currentPriority = strafePriority;
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

        if (!m_enemy.checkWithinRange(distToStrafe, m_playerMask))
        {
            return false;
        }

        if (m_playerTransform != null)
        {
            float dist = m_enemy.getDistance(m_playerTransform);
            if (dist <= distToStrafe)
            {
                return true;
            }
            else if (dist > distToStrafe)
            {
                return false;
            }
        }
        if (m_enemy.checkWithinRange(distToStrafe, m_playerMask))
        {
            return true;
        }
        return false;
    }
}
