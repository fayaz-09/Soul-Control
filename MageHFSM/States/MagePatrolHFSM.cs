using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePatrolHFSM : BaseStatesHFSM
{
    private LayerMask m_playerMask;
    private MageEnemy enemy;

    public MagePatrolHFSM(MageHSM stateMachine, MageEnemy en) : base("PatrolState", stateMachine)
    {
        enemy = en;
    }

    public override void Enter()
    {
        Debug.Log("Patrol");
        m_playerMask = LayerMask.GetMask("Player");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemy.patrol();

        if (enemy.checkWithinRange(MageHSM.fovRange, m_playerMask))
        {
            stateMachine.ChangeState(((MageHSM)stateMachine).chaseState);
        }

    }
}
