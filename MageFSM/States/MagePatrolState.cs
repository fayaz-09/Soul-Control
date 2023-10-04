using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePatrolState : BaseStates
{
    private LayerMask m_playerMask;
    private MageEnemy enemy;

    public MagePatrolState(MageSM stateMachine, MageEnemy en) : base("PatrolState", stateMachine)
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

        if (enemy.checkWithinRange(MageSM.fovRange, m_playerMask))
        {
            stateMachine.ChangeState(((MageSM)stateMachine).chaseState);
        }

    }
}
