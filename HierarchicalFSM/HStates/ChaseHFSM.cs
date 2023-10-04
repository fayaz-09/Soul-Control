using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChaseHFSM : CombatHFSM
{

    public ChaseHFSM(EnemyHSM stateMachine, Enemy en) : base("ChaseHFSM", stateMachine, en)
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
        if (dist <= EnemyHSM.circleRange)
        {
            stateMachine.ChangeState(((EnemyHSM)stateMachine).strafeState);
        }
        else if (dist > EnemyHSM.circleRange && dist <= EnemyHSM.fovRange)
        {
            enemy.chaseTarget(m_playerTransform);
        }
    }
}
