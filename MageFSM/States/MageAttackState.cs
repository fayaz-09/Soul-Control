using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAttackState : BaseStates
{

    private LayerMask m_playerMask;
    private Transform m_playerTransform;

    private MageEnemy enemy;

    public MageAttackState(MageSM stateMachine, MageEnemy en) : base("AttackFSM", stateMachine)
    {
        enemy = en;
    }


    public override void Enter()
    {
        Debug.Log("Attack");
        m_playerMask = LayerMask.GetMask("Player");
        if (enemy.checkWithinRange(MageSM.fovRange, m_playerMask))
        {
            m_playerTransform = enemy.getTargetInRange(MageSM.fovRange, m_playerMask);
        }
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);
        if (dist <= MageSM.attackRange && enemy.canAttack)
        {
            enemy.rangeAttack(m_playerTransform);
        }
        else
        {
            Debug.Log("end attack state");
            stateMachine.ChangeState(((MageSM)stateMachine).strafeState);
        }
    }
}
