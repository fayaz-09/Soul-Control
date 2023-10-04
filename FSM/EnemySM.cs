using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemySM : StateMachine
{
    [HideInInspector]
    public PatrolFSM patrolState;
    [HideInInspector]
    public ChaseFSM chaseState;
    [HideInInspector]
    public AttackFSM attackState;
    [HideInInspector]
    public StrafeFSM strafeState;

    public static float fovRange = 35f;
    public static float circleRange = 10f;
    public static float attackRange = 4f;


    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        patrolState = new PatrolFSM(this, enemy);
        chaseState = new ChaseFSM(this, enemy);
        attackState = new AttackFSM(this, enemy);
        strafeState = new StrafeFSM(this, enemy);
    }

    protected override BaseStates GetInitialState()
    {
        return patrolState;
    }
}
