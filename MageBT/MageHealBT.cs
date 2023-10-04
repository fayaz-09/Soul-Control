using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class MageHealBT : Node
{
    private MageEnemy m_enemy;
    LayerMask allyMask;
    List<GameObject> allyList;

    public MageHealBT(MageEnemy enemy)
    {
        m_enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Heal ally");
        allyMask = LayerMask.GetMask("Enemy");
        allyList = m_enemy.getAlliesInRange(MageBT.healRange, allyMask);
        m_enemy.healLowest(allyList);
        state = NodeState.RUNNING;
        return state;
    }
}
