using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseFSM : BaseStates
{
    private LayerMask m_playerMask;
    private Transform m_playerTransform;
    private Enemy enemy;

    public ChaseFSM(EnemySM stateMachine,Enemy en) : base("ChaseFSM", stateMachine)
    {
        enemy = en;
    }

    public override void Enter()
    {
        m_playerMask = LayerMask.GetMask("Player");

        if(enemy.checkWithinRange(EnemySM.fovRange, m_playerMask))
        {
            m_playerTransform = enemy.getTargetInRange(EnemySM.fovRange, m_playerMask);
        }
        

        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);
        if (dist <= EnemySM.circleRange)
        {
            stateMachine.ChangeState(((EnemySM)stateMachine).strafeState);
        }
        else if (dist > EnemySM.circleRange && dist <= EnemySM.fovRange)
        {
            enemy.chaseTarget(m_playerTransform);
        }
        else if (dist > EnemySM.fovRange)
        {
            stateMachine.ChangeState(((EnemySM)stateMachine).patrolState);
        }
    }
}
