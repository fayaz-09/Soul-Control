using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class CheckInAttackRangeBT : Node
{
    private Transform m_agentTransform;
    private LayerMask m_playerMask;
    private Enemy enemy;

    public CheckInAttackRangeBT(Transform transform)
    {
        m_agentTransform = transform;
        enemy = transform.GetComponent<Enemy>();
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
        if(Vector3.Distance(m_agentTransform.position, target.position) <= EnemyBT.attackRange && enemy.canAttack)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
