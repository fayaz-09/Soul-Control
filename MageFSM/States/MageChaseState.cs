using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageChaseState : BaseStates
{
    private LayerMask m_playerMask;
    private Transform m_playerTransform;
    private MageEnemy enemy;

    public MageChaseState(MageSM stateMachine, MageEnemy en) : base("ChaseState", stateMachine)
    {
        enemy = en;
    }

    public override void Enter()
    {
        Debug.Log("Chase");
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
        if (dist <= MageSM.circleRange)
        {
            Debug.Log("in strafe range");
            stateMachine.ChangeState(((MageSM)stateMachine).strafeState);
        }
        else if (dist > MageSM.circleRange && dist <= MageSM.fovRange)
        {
            enemy.chaseTarget(m_playerTransform);
        }
        else if (dist > MageSM.fovRange)
        {
            Debug.Log("Out of range");
            stateMachine.ChangeState(((MageSM)stateMachine).patrolState);
        }
    }
}
