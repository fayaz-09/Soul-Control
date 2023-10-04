using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAction_Idle : MageAction_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(MageGoal_Idle) });
    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float getCost()
    {
        return 0f;
    }
}
