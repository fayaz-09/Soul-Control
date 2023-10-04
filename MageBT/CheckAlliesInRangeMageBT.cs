using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckAlliesInRangeMageBT : Node
{
    MageEnemy enemy;
    LayerMask allyMask;
    List<GameObject> allyList;
    public CheckAlliesInRangeMageBT(MageEnemy en)
    {
        enemy = en;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Check if can heal");
        allyMask = LayerMask.GetMask("Enemy");
        
        if(enemy.canHeal)
        {
            if (enemy.allyCheck(MageBT.healRange, allyMask))
            {
                allyList = enemy.getAlliesInRange(MageBT.healRange, allyMask);
                if (enemy.checkHealth(allyList))
                {
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
        }
        
        state = NodeState.FAILURE;
        return state;
    }
}
