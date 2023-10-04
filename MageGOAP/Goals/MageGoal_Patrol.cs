using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageGoal_Patrol : MageGoal_Base
{
    [SerializeField] int maxPriority = 30;

    [SerializeField] float PriorityBuildRate = 1f;
    [SerializeField] float PriorityDecayRate = 0.1f;
    float currentPriority = 0f;

    public override void onTickGoal()
    {
        if (m_enemy.isMoving)
        {
            currentPriority -= PriorityDecayRate * Time.deltaTime;
        }
        else
        {
            currentPriority += PriorityBuildRate * Time.deltaTime;
        }
    }

    public override void onGoalActivated(MageAction_Base linkedAction)
    {
        base.onGoalActivated(linkedAction);
        currentPriority = maxPriority;
    }

    public override float onCaclculatePriority()
    {
        return Mathf.FloorToInt(currentPriority);
    }

    public override bool canRun()
    {
        return true;
    }
}
