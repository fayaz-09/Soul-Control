using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using BehaviourTree;

public class EnemyBT : TreeBT
{
    //public static float speed = 2f;
    public static float fovRange = 35f;
    public static float circleRange = 10f;
    public static float attackRange = 4f;
    private Enemy enemy;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckInAttackRangeBT(transform),
                new EnemyAttackBT(enemy),
            }),
            new Sequence(new List<Node>
            {
                new CheckInStrafeBT(transform),
                new StrafeBT(enemy),
            }),
            new Sequence(new List<Node>
            {
                new CheckInRangeBT(transform),
                new GoToPlayerBT(enemy),
            }),
            new PatrolBT(enemy),
        });
        return root;
    }
}
