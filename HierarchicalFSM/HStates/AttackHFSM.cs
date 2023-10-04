using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackHFSM : CombatHFSM
{
    public AttackHFSM(EnemyHSM stateMachine, Enemy en) : base("AttackHFSM", stateMachine, en)
    {
        
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);
        if (dist <= EnemyHSM.attackRange && enemy.canAttack)
        {
            enemy.attackTarget(m_playerTransform);
        }
        else
        {
            stateMachine.ChangeState(((EnemyHSM)stateMachine).strafeState);
        }
    }
}
