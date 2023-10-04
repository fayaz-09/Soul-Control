using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHealState : BaseStates
{

    private LayerMask allyMask;
    private List<GameObject> allyList;

    private MageEnemy enemy;

    public MageHealState(MageSM stateMachine, MageEnemy en) : base("HealFSM", stateMachine)
    {
        enemy = en;
    }


    public override void Enter()
    {
        Debug.Log("Heal ally");
        allyMask = LayerMask.GetMask("Enemy");
        if (enemy.allyCheck(MageSM.healRange, allyMask))
        {
            allyList = enemy.getAlliesInRange(MageSM.healRange, allyMask);
        }
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.canHeal)
        {
            if (enemy.checkHealth(allyList))
            {
                enemy.healLowest(allyList);
            }
        }
        else
        {
            Debug.Log("end heal state");
            stateMachine.ChangeState(((MageSM)stateMachine).patrolState);
        }
    }
}
