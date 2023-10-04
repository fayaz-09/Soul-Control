using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStrafeState : BaseStates
{
    private LayerMask m_playerMask;
    private Transform m_playerTransform;
    private MageEnemy enemy;

    private LayerMask allyMask;
    private List<GameObject> allyList;

    public MageStrafeState(MageSM stateMachine, MageEnemy en) : base("StrafeState", stateMachine)
    {
        enemy = en;
    }

    public override void Enter()
    {
        Debug.Log("Strafe");
        m_playerMask = LayerMask.GetMask("Player");

        if (enemy.checkWithinRange(MageSM.fovRange, m_playerMask))
        {
            m_playerTransform = enemy.getTargetInRange(MageSM.fovRange, m_playerMask);
        }

        allyMask = LayerMask.GetMask("Enemy");
        if (enemy.allyCheck(MageSM.healRange, allyMask))
        {
            allyList = enemy.getAlliesInRange(MageSM.healRange, allyMask);
        }


        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        float dist = enemy.getDistance(m_playerTransform);

        if (enemy.canHeal)
        {
            if (enemy.allyCheck(MageSM.healRange, allyMask))
            {
                allyList = enemy.getAlliesInRange(MageSM.healRange, allyMask);
                if (enemy.checkHealth(allyList))
                {
                    stateMachine.ChangeState(((MageSM)stateMachine).healState);
                }
            }
        }

        if (dist <= MageSM.attackRange && enemy.canAttack)
        {
            Debug.Log("in attack range");
            stateMachine.ChangeState(((MageSM)stateMachine).attackState);
        }
        else if (dist <= MageSM.circleRange)
        {
            if (enemy.getCurrentStrafeTime() >= enemy.minStrafeTime)
            {
                enemy.chaseTarget(m_playerTransform);
                enemy.setStrafeFalse();
                enemy.canAttack = true;
            }
            else
            {
                enemy.strafeAroundTarget(m_playerTransform, MageSM.circleRange);
                enemy.setStrafeTrue();
            }
        }
        else if (dist > MageSM.circleRange)
        {
            Debug.Log("Out of range");
            enemy.setStrafeFalse();
            stateMachine.ChangeState(((MageSM)stateMachine).chaseState);
        }
    }
}
