using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOAPPlanner : MonoBehaviour
{
    Goal_Base[] Goals;
    Action_Base[] Actions;

    Goal_Base ActiveGoal;
    Action_Base ActiveAction;

    private void Awake()
    {
        Goals = GetComponents<Goal_Base>();
        Actions = GetComponents<Action_Base>();
    }

    // Update is called once per frame
    void Update()
    {
        Goal_Base bestGoal = null;
        Action_Base bestAction = null;

        //find highest priority goal that can activate
        foreach (var goal in Goals)
        {
            goal.onTickGoal();

            //can goal run
            if(!goal.canRun())
            {
                continue;
            }
            // is it a worse priority
            if(!(bestGoal == null || goal.onCaclculatePriority() > bestGoal.onCaclculatePriority()))
            {
                continue;
            }

            Action_Base candidateAction = null;
            foreach(var action in Actions)
            {
                if(!action.GetSupportedGoals().Contains(goal.GetType()))
                {
                    continue;
                }
                if(candidateAction == null ||  action.getCost() < candidateAction.getCost())
                {
                    candidateAction = action;
                }
            }

            if(candidateAction != null)
            {
                bestGoal = goal;
                bestAction = candidateAction;
            }
        }

        //if no current goal
        if(ActiveGoal == null)
        {
            ActiveGoal = bestGoal;
            ActiveAction = bestAction;
            
            if (ActiveGoal != null)
            {
                ActiveGoal.onGoalActivated(ActiveAction);
            }
            if (ActiveAction != null)
            {
                ActiveAction.onActivated(ActiveGoal);
            }
        }
        //no change in goal
        else if(ActiveGoal == bestGoal)
        {
            if(ActiveAction != bestAction)
            {
                ActiveAction.onDeactivated();
                ActiveAction = bestAction;

                ActiveAction.onActivated(ActiveGoal);
            }
        }
        //new goal or no valid goal
        else if(ActiveGoal != bestGoal)
        {
            ActiveGoal.onGoalDeactivated();
            ActiveAction.onDeactivated();

            ActiveGoal = bestGoal;
            ActiveAction = bestAction;

            if(ActiveGoal != null)
            {
                ActiveGoal.onGoalActivated(ActiveAction);
            }
            if(ActiveAction != null)
            { 
                ActiveAction.onActivated(ActiveGoal);
            }
        }

        if(ActiveAction != null)
        {
            ActiveAction.onTick();
        }

    }
}
