using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Chase : Goal_Base
{
    [SerializeField] int chasePriority = 60;
    [SerializeField] float distAwarenessToChase = 35f;

    [SerializeField] public Transform m_playerTransform;
    int currentPriority = 0;



    public override void onTickGoal()
    {
        currentPriority = 0;

        if (!m_enemy.checkWithinRange(distAwarenessToChase, m_playerMask))
        {
            m_playerTransform = null;
            return;
        }


        if (m_playerTransform != null)
        {
            float dist = m_enemy.getDistance(m_playerTransform);
            if (dist <= distAwarenessToChase)
            {
                currentPriority = chasePriority;
                return;
            }
            else if(dist >  distAwarenessToChase)
            {
                currentPriority = 0;
                m_playerTransform = null;
                return;
            }
            
        }
       
        if (m_enemy.checkWithinRange(distAwarenessToChase, m_playerMask))
        {
            m_playerTransform = m_enemy.getTargetInRange(distAwarenessToChase, m_playerMask);
            currentPriority = chasePriority;
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
        
        if (!m_enemy.checkWithinRange(distAwarenessToChase, m_playerMask))
        {
            return false;
        }

        if (m_playerTransform != null)
        {
            float dist = m_enemy.getDistance(m_playerTransform);
            if (dist <= distAwarenessToChase)
            {
                return true;
            }
            else if (dist > distAwarenessToChase)
            {
                return false;
            }

        }
        if (m_enemy.checkWithinRange(distAwarenessToChase, m_playerMask))
        {
            return true;
        }

        return false;
        
        
    }
}
