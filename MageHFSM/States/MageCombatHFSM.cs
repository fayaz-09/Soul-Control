using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageCombatHFSM : BaseStatesHFSM
{
    protected LayerMask m_playerMask;
    protected Transform m_playerTransform;

    protected MageEnemy enemy;


    public MageCombatHFSM(string name, MageHSM stateMachine, MageEnemy en) : base(name, stateMachine)
    {
        enemy = en;
    }


    public override void Enter()
    {
        m_playerMask = LayerMask.GetMask("Player");

        if (enemy.checkWithinRange(MageHSM.fovRange, m_playerMask))
        {
            m_playerTransform = enemy.getTargetInRange(MageHSM.fovRange, m_playerMask);
        }

        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        float dist = enemy.getDistance(m_playerTransform);
        if (dist > MageHSM.fovRange)
        {
            Debug.Log("Out of range");
            stateMachine.ChangeState(((MageHSM)stateMachine).patrolState);
        }
    }
}
