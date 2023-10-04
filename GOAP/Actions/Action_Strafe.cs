using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Strafe : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Strafe) });

    Goal_Strafe strafeGoal;

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float getCost()
    {
        return 0f;
    }

    public override void onActivated(Goal_Base linkedGoal)
    {
        base.onActivated(linkedGoal);
        strafeGoal = (Goal_Strafe)LinkedGoal;

        if (m_enemy.getCurrentStrafeTime() >= m_enemy.minStrafeTime)
        {
            m_enemy.chaseTarget(strafeGoal.m_playerTransform);
            m_enemy.setStrafeFalse();
            m_enemy.canAttack = true;
        }
        else
        {
            m_enemy.strafeAroundTarget(strafeGoal.m_playerTransform, 10f);
            m_enemy.setStrafeTrue();
        }
    }

    public override void onDeactivated()
    {
        strafeGoal = null;
    }

    public override void onTick()
    {
        if (m_enemy.getCurrentStrafeTime() >= m_enemy.minStrafeTime)
        {
            m_enemy.chaseTarget(strafeGoal.m_playerTransform);
            m_enemy.setStrafeFalse();
            m_enemy.canAttack = true;
        }
        else
        {
            m_enemy.strafeAroundTarget(strafeGoal.m_playerTransform, 10f);
            m_enemy.setStrafeTrue();
        }
    }
}
