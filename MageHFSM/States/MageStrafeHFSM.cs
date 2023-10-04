using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStrafeHFSM : MageCombatHFSM
{
    private LayerMask allyMask;
    private List<GameObject> allyList;

    public MageStrafeHFSM(MageHSM stateMachine, MageEnemy en) : base("StrafeHFSM", stateMachine, en)
    {

    }

    public override void Enter()
    {
        allyMask = LayerMask.GetMask("Enemy");
        if (enemy.allyCheck(MageHSM.healRange, allyMask))
        {
            allyList = enemy.getAlliesInRange(MageHSM.healRange, allyMask);
        }

        base.Enter();
        Debug.Log("Strafe");
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);

        if (enemy.canHeal)
        {
            if (enemy.allyCheck(MageHSM.healRange, allyMask))
            {
                allyList = enemy.getAlliesInRange(MageHSM.healRange, allyMask);
                if (enemy.checkHealth(allyList))
                {
                    stateMachine.ChangeState(((MageHSM)stateMachine).healState);
                }
            }
        }

        if (dist <= MageHSM.attackRange && enemy.canAttack)
        {
            Debug.Log("in attack range");
            stateMachine.ChangeState(((MageHSM)stateMachine).attackState);
        }
        else if (dist <= MageHSM.circleRange)
        {
            if (enemy.getCurrentStrafeTime() >= enemy.minStrafeTime)
            {
                enemy.chaseTarget(m_playerTransform);
                enemy.setStrafeFalse();
                enemy.canAttack = true;
            }
            else
            {
                enemy.strafeAroundTarget(m_playerTransform, MageHSM.circleRange);
                enemy.setStrafeTrue();
            }
        }
        else
        {
            Debug.Log("Out of range: begin chase");
            enemy.setStrafeFalse();
            stateMachine.ChangeState(((MageHSM)stateMachine).chaseState);
        }
    }
}
