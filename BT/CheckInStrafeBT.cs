using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class CheckInStrafeBT : Node
{
    private Transform m_agentTransform;
    private LayerMask m_playerMask;
    
    public CheckInStrafeBT(Transform transform)
    {
        m_agentTransform = transform;
    }
    
    public override NodeState Evaluate()
    {
        Debug.Log("Check in strafe range");
        object t = GetData("target");
        m_playerMask = LayerMask.GetMask("Player");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }
    
        Transform target = (Transform)t;
        if (Vector3.Distance(m_agentTransform.position, target.position) <= EnemyBT.circleRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }
    
        state = NodeState.FAILURE;
        return state;
    }
}
