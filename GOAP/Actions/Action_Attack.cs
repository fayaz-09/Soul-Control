using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Attack : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Attack) });

    Goal_Attack attackGoal;

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
        attackGoal = (Goal_Attack)LinkedGoal;

        m_enemy.attackTarget(attackGoal.m_playerTransform);
    }

    public override void onDeactivated()
    {
        attackGoal = null;
    }

    public override void onTick()
    {
        m_enemy.attackTarget(attackGoal.m_playerTransform);
    }
}
