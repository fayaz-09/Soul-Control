using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHSM : HierarchicalStateMachine
{
    [HideInInspector]
    public PatrolHFSM patrolState;
    [HideInInspector]
    public ChaseHFSM chaseState;
    [HideInInspector]
    public AttackHFSM attackState;
    [HideInInspector]
    public StrafeHFSM strafeState;

    public static float fovRange = 35f;
    public static float circleRange = 10f;
    public static float attackRange = 4f;

    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        patrolState = new PatrolHFSM(this, enemy);
        chaseState = new ChaseHFSM(this, enemy);
        attackState = new AttackHFSM(this, enemy);
        strafeState = new StrafeHFSM(this, enemy);
    }

    protected override BaseStatesHFSM GetInitialState()
    {
        return patrolState;
    }
}
