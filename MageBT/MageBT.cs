using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class MageBT : TreeBT
{
    //public static float speed = 2f;
    public static float fovRange = 35f;
    public static float circleRange = 15f;
    public static float attackRange = 15f;
    public static float healRange = 18f;
    private MageEnemy enemy;
    private void Awake()
    {
        enemy = GetComponent<MageEnemy>();
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckAlliesInRangeMageBT(enemy),
                new MageHealBT(enemy),
            }),
            new Selector(new List<Node>()
            {
                new Sequence(new List<Node>
                {
                new CheckInProjectileAttackRangeMageBT(transform),
                new MageAttackBT(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckInStrafeMageBT(transform),
                    new StrafeMageBT(enemy),
                }),
                new Sequence(new List<Node>
                {
                    new CheckInRangeMageBT(transform),
                    new ChaseMageBT(enemy),
                }),
             }),
            new MagePatrolBT(enemy),

        });
        return root;
    }
}
