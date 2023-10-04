using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSM : StateMachine
{
    [HideInInspector]
    public MagePatrolState patrolState;
    [HideInInspector]
    public MageChaseState chaseState;
    [HideInInspector]
    public MageAttackState attackState;
    [HideInInspector]
    public MageStrafeState strafeState;
    [HideInInspector]
    public MageHealState healState;

    public static float fovRange = 35f;
    public static float circleRange = 15f;
    public static float attackRange = 15f;
    public static float healRange = 18f;


    private MageEnemy enemy;

    private void Awake()
    {
        enemy = GetComponent<MageEnemy>();

        patrolState = new MagePatrolState(this, enemy);
        chaseState = new MageChaseState(this, enemy);
        attackState = new MageAttackState(this, enemy);
        strafeState = new MageStrafeState(this, enemy);
        healState = new MageHealState(this, enemy);
    }

    protected override BaseStates GetInitialState()
    {
        return patrolState;
    }
}
