using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{

    [SerializeField] private Transform movePostransform;
    private NavMeshAgent navAgent;
   
    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            navAgent.destination = movePostransform.position;
        }
    }
}
