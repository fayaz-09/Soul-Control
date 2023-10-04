using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHealHFSM : BaseStatesHFSM
{
    private LayerMask m_playerMask;
    private MageEnemy enemy;

    private LayerMask allyMask;
    private List<GameObject> allyList;

    public MageHealHFSM(MageHSM stateMachine, MageEnemy en) : base("PatrolState", stateMachine)
    {
        enemy = en;
    }

    public override void Enter()
    {
        Debug.Log("Heal");
        m_playerMask = LayerMask.GetMask("Player");

        allyMask = LayerMask.GetMask("Enemy");
        if (enemy.allyCheck(MageHSM.healRange, allyMask))
        {
            allyList = enemy.getAlliesInRange(MageHSM.healRange, allyMask);
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
            stateMachine.ChangeState(((MageHSM)stateMachine).patrolState);
        }

    }
}
