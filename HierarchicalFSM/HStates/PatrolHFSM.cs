using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolHFSM : BaseStatesHFSM
{
    private LayerMask m_playerMask;
    private Enemy enemy;

    public PatrolHFSM(EnemyHSM stateMachine, Enemy en) : base("PatrolHFSM", stateMachine) 
    {
        enemy = en;
    }

    public override void Enter()
    {
        m_playerMask = LayerMask.GetMask("Player");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemy.patrol();

        if (enemy.checkWithinRange(EnemyHSM.fovRange, m_playerMask))
        {
            stateMachine.ChangeState(((EnemyHSM)stateMachine).chaseState);
        }

    }
}
