using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatHFSM : BaseStatesHFSM
{
    protected LayerMask m_playerMask;
    protected Transform m_playerTransform;
    
    protected Enemy enemy;
   
   
    public CombatHFSM(string name, EnemyHSM stateMachine, Enemy en) : base(name, stateMachine)
    {
        enemy = en;
    }


    public override void Enter()
    {
        m_playerMask = LayerMask.GetMask("Player");

        if (enemy.checkWithinRange(EnemyHSM.fovRange, m_playerMask))
        {
            m_playerTransform = enemy.getTargetInRange(EnemyHSM.fovRange, m_playerMask);
        }

        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        float dist = enemy.getDistance(m_playerTransform);
        if (dist > EnemyHSM.fovRange)
        {
            stateMachine.ChangeState(((EnemyHSM)stateMachine).patrolState);
        }
    }
}
