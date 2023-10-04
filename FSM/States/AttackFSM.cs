using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackFSM : BaseStates
{
    
    private LayerMask m_playerMask;
    private Transform m_playerTransform;

    private Enemy enemy;

    public AttackFSM(EnemySM stateMachine, Enemy en) : base("AttackFSM", stateMachine)
    {
        enemy = en;
    }


    public override void Enter()
    {
        m_playerMask = LayerMask.GetMask("Player");
        if (enemy.checkWithinRange(EnemySM.fovRange, m_playerMask))
        {
            m_playerTransform = enemy.getTargetInRange(EnemySM.fovRange, m_playerMask);
        }
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);
        if (dist <= EnemySM.attackRange && enemy.canAttack)
        {
            enemy.attackTarget(m_playerTransform);
        }
        else
        {
            stateMachine.ChangeState(((EnemySM)stateMachine).strafeState);
        }
    }
}
