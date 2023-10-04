using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageGoal_Idle : MageGoal_Base
{
    [SerializeField] int Priority = 10;
    public override float onCaclculatePriority()
    {
        return Priority;
    }

    public override bool canRun()
    {
        return true;
    }
}
