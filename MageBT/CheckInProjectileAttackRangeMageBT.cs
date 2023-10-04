using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;


public class CheckInProjectileAttackRangeMageBT : Node
{
    private Transform m_agentTransform;
    private LayerMask m_playerMask;
    private MageEnemy enemy;

    public CheckInProjectileAttackRangeMageBT(Transform transform)
    {
        m_agentTransform = transform;
        enemy = transform.GetComponent<MageEnemy>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Check in attack range");
        object t = GetData("target");
        m_playerMask = LayerMask.GetMask("Player");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(m_agentTransform.position, target.position) <= MageBT.attackRange && enemy.canAttack)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
