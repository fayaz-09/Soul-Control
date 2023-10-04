using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAction_Base : MonoBehaviour
{
    protected LayerMask m_playerMask;
    protected MageGoal_Base LinkedGoal;
    protected MageEnemy m_enemy;

    private void Awake()
    {
        m_playerMask = LayerMask.GetMask("Player");
        m_enemy = GetComponent<MageEnemy>();
    }

    public virtual List<System.Type> GetSupportedGoals()
    {
        return null;
    }

    public virtual float getCost()
    {
        return 0f;
    }

    public virtual void onActivated(MageGoal_Base linkedGoal)
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
