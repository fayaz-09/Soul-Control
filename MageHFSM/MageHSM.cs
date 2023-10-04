using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageHSM : HierarchicalStateMachine
{
    [HideInInspector]
    public MagePatrolHFSM patrolState;
    [HideInInspector]
    public MageChaseHFSM chaseState;
    [HideInInspector]
    public MageAttackHFSM attackState;
    [HideInInspector]
    public MageStrafeHFSM strafeState;
    [HideInInspector]
    public MageHealHFSM healState;

    public static float fovRange = 35f;
    public static float circleRange = 15f;
    public static float attackRange = 15f;
    public static float healRange = 18f;


    private MageEnemy enemy;

    private void Awake()
    {
        enemy = GetComponent<MageEnemy>();

        patrolState = new MagePatrolHFSM(this, enemy);
        chaseState = new MageChaseHFSM(this, enemy);
        attackState = new MageAttackHFSM(this, enemy);
        strafeState = new MageStrafeHFSM(this, enemy);
        healState = new MageHealHFSM(this, enemy);
    }

    protected override BaseStatesHFSM GetInitialState()
    {
        return patrolState;
    }
}
