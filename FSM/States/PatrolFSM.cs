using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PatrolFSM : BaseStates
{
    private LayerMask m_playerMask;
    private Enemy enemy;

    public PatrolFSM(EnemySM stateMachine, Enemy en) : base("PatrolFSM", stateMachine) 
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

        if(enemy.checkWithinRange(EnemySM.fovRange, m_playerMask))
        {
            stateMachine.ChangeState(((EnemySM)stateMachine).chaseState);
        }

    }
}
