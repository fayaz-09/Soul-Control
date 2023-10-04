using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackHFSM : MageCombatHFSM
{
    public MageAttackHFSM(MageHSM stateMachine, MageEnemy en) : base("AttackHFSM", stateMachine, en)
    {

    }


    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attack");
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);
        if (dist <= EnemyHSM.attackRange && enemy.canAttack)
        {
            enemy.rangeAttack(m_playerTransform);
        }
        else
        {
            Debug.Log("end attack state");
            stateMachine.ChangeState(((MageHSM)stateMachine).strafeState);
        }
    }
}
