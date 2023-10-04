using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAction_Heal : MageAction_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(MageGoal_Heal) });

    MageGoal_Heal healGoal;

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
        healGoal = (MageGoal_Heal)LinkedGoal;

        m_enemy.healLowest(healGoal.allyList);
    }

    public override void onDeactivated()
    {
        healGoal = null;
    }

    public override void onTick()
    {
        m_enemy.healLowest(healGoal.allyList);
    }
}
