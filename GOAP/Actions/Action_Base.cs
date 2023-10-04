using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Action_Base : MonoBehaviour
{
    protected LayerMask m_playerMask;
    protected Goal_Base LinkedGoal;
    protected Enemy m_enemy;

    private void Awake()
    {
        m_playerMask = LayerMask.GetMask("Player");
        m_enemy = GetComponent<Enemy>();
    }

    public virtual List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    public virtual float getCost()
    {
        return 0f;
    }

    public virtual void onActivated(Goal_Base linkedGoal)
    {
        LinkedGoal = linkedGoal;
    }

    public virtual void onDeactivated()
    {
        LinkedGoal = null;
    }

    public virtual void onTick()
    {

    }
}
