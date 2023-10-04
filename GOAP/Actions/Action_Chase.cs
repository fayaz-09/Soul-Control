using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Chase : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Chase) });

    Goal_Chase chaseGoal;

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
        chaseGoal = (Goal_Chase)LinkedGoal;
        m_enemy.setStrafeFalse();
        m_enemy.chaseTarget(chaseGoal.m_playerTransform);
    }

    public override void onDeactivated()
    {
        chaseGoal = null;
    }

    public override void onTick()
    {
        m_enemy.chaseTarget(chaseGoal.m_playerTransform);
    }
}
