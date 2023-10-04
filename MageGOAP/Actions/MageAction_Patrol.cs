using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAction_Patrol : MageAction_Base
{
    int m_currentWaypoint = 0;
    Transform currentDestination;

    float m_maxWait = 2f;
    float m_waitTime = 0f;
    bool m_waiting = false;

    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(MageGoal_Patrol) });
    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float getCost()
    {
        return 0f;
    }

    public override void onActivated(MageGoal_Base linkedGoal)
    {
        base.onActivated(linkedGoal);
        if (currentDestination != null)
        {
            m_enemy.chaseTarget(currentDestination);
        }
    }

    public override void onDeactivated()
    {

    }

    public override void onTick()
    {
        if (m_waiting)
        {
            m_waitTime += Time.deltaTime;
            if (m_waitTime >= m_maxWait)
            {
                m_waiting = false;
            }
        }
        else
        {
            currentDestination = m_enemy.waypoints[m_currentWaypoint];
            if (m_enemy.getDistance(currentDestination) <= 3f)
            {
                m_waitTime = 0f;
                m_waiting = true;
                m_currentWaypoint = (m_currentWaypoint + 1) % m_enemy.waypoints.Length;
            }
            else
            {
                onActivated(LinkedGoal);
            }
        }
    }
}
