using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;


public class CheckInRangeMageBT : Node
{
    private Transform m_agentTransform;
    private LayerMask m_playerMask;

    public CheckInRangeMageBT(Transform transform)
    {
        m_agentTransform = transform;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Check in range");
        object t = GetData("target");
        m_playerMask = LayerMask.GetMask("Player");
        if (t == null)
        {
            Collider[] colliders = Physics.OverlapSphere(m_agentTransform.position, MageBT.fovRange, m_playerMask);
            if (colliders.Length > 0)
            {

                parent.parent.SetData("target", colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;

            }

            state = NodeState.FAILURE;
            return state;
        }
        else
        {
            Transform target = (Transform)t;
            if (Vector3.Distance(m_agentTransform.position, target.position) >= MageBT.fovRange)
            {
                ClearData("target");
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}