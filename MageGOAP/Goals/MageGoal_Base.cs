using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MageIGoal
{
    float onCaclculatePriority();
    bool canRun();
    void onTickGoal();
    void onGoalActivated(MageAction_Base linkedAction);
    void onGoalDeactivated();
}

public class MageGoal_Base : MonoBehaviour, MageIGoal
{
    protected LayerMask m_playerMask;
    protected LayerMask allyMask;
    protected GOAPUI DebugUI;
    protected MageAction_Base LinkedAction;
    protected MageEnemy m_enemy;

    void Awake()
    {
        m_playerMask = LayerMask.GetMask("Player");
        allyMask = LayerMask.GetMask("Enemy");
        m_enemy = GetComponent<MageEnemy>();
    }

    void Start()
    {
        DebugUI = FindObjectOfType<GOAPUI>();
    }

    void Update()
    {
        onTickGoal();
        DebugUI.UpdateGoal(this, GetType().Name, LinkedAction ? "Running" : "Paused", onCaclculatePriority());
    }

    public virtual float onCaclculatePriority()
    {
        return -1;
    }

    public virtual bool canRun()
    {
        return false;
    }

    public virtual void onTickGoal()
    {

    }

    public virtual void onGoalActivated(MageAction_Base linkedAction)
    {
        LinkedAction = linkedAction;
    }

    public virtual void onGoalDeactivated()
    {
        LinkedAction = null;
    }
}
