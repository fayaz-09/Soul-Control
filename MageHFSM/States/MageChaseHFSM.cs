using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageChaseHFSM : MageCombatHFSM
{

    public MageChaseHFSM(MageHSM stateMachine, MageEnemy en) : base("ChaseHFSM", stateMachine, en)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Chase");
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);
        if (dist <= MageHSM.circleRange)
        {
            Debug.Log("in strafe range");
            stateMachine.ChangeState(((MageHSM)stateMachine).strafeState);
        }
        else if (dist > MageHSM.circleRange)
        {
            enemy.chaseTarget(m_playerTransform);
        }
    }
}
