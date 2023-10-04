using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeHFSM : CombatHFSM
{

    public StrafeHFSM(EnemyHSM stateMachine, Enemy en) : base("StrafeHFSM", stateMachine, en)
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
            stateMachine.ChangeState(((EnemyHSM)stateMachine).attackState);
        }
        else if (dist <= EnemyHSM.circleRange)
        {
            if (enemy.getCurrentStrafeTime() >= enemy.minStrafeTime)
            {
                enemy.chaseTarget(m_playerTransform);
                enemy.setStrafeFalse();
                enemy.canAttack = true;
            }
            else
            {
                enemy.strafeAroundTarget(m_playerTransform, EnemyHSM.circleRange);
                enemy.setStrafeTrue();
            }
        }
        else if (dist > EnemySM.circleRange)
        {
            enemy.setStrafeFalse();
            stateMachine.ChangeState(((EnemyHSM)stateMachine).chaseState);
        }
    }
}
