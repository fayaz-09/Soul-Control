using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeFSM : BaseStates
{
    private LayerMask m_playerMask;
    private Transform m_playerTransform;
    private Enemy enemy;

    public StrafeFSM(EnemySM stateMachine, Enemy en) : base("StrafeFSM", stateMachine)
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
            stateMachine.ChangeState(((EnemySM)stateMachine).attackState);
        }
        else if (dist <= EnemySM.circleRange)
        {
            if (enemy.getCurrentStrafeTime() >= enemy.minStrafeTime)
            {
                enemy.chaseTarget(m_playerTransform);
                enemy.setStrafeFalse();
                enemy.canAttack = true;
            }
            else
            {
                enemy.strafeAroundTarget(m_playerTransform, EnemySM.circleRange);
                enemy.setStrafeTrue();
            }
        }
        else if (dist > EnemySM.circleRange)
        {
            enemy.setStrafeFalse();
            stateMachine.ChangeState(((EnemySM)stateMachine).chaseState);
        }
    }
}
