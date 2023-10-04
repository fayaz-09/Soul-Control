using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageGoal_Heal : MageGoal_Base
{
    [SerializeField] int healPriority = 90;
    [SerializeField] float distToheal = 18f;
    public List<GameObject> allyList;
    int currentPriority = 0;

    public override void onTickGoal()
    {
        currentPriority = 0;
        if (!m_enemy.allyCheck(distToheal, allyMask))
        {
            allyList = null;
            return;
        }

        if (allyList != null)
        {
            if (m_enemy.canHeal)
            {
                if (m_enemy.checkHealth(allyList))
                {
                    currentPriority = healPriority;
                    return;
                }
            }
            else
            {
                currentPriority = 0;
                allyList = null;
                return;
            }
        }

        if (m_enemy.allyCheck(distToheal, allyMask))
        {
            allyList = m_enemy.getAlliesInRange(distToheal, allyMask);
            if (m_enemy.canHeal)
            {
                if (m_enemy.checkHealth(allyList))
                {
                    currentPriority = healPriority;
                    return;
                }
            }
        }
    }

    public override void onGoalDeactivated()
    {
        base.onGoalDeactivated();
        allyList = null;
    }

    public override float onCaclculatePriority()
    {
        return currentPriority;
    }

    public override bool canRun()
    {
        if (!m_enemy.allyCheck(distToheal, allyMask))
        {
            return false;
        }
        
        if (allyList != null)
        {
            if (m_enemy.canHeal)
            {
                if (m_enemy.checkHealth(allyList))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        if (m_enemy.allyCheck(distToheal, allyMask))
        {
            return true;
        }
        return false;
    }
}
