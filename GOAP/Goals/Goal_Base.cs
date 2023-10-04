using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IGoal
{
    float onCaclculatePriority();
    bool canRun();
    void onTickGoal();
    void onGoalActivated(Action_Base linkedAction);
    void onGoalDeactivated();
}

public class Goal_Base : MonoBehaviour, IGoal
{
    protected LayerMask m_playerMask;
    protected GOAPUI DebugUI;
    protected Action_Base LinkedAction;
    protected Enemy m_enemy;

    void Awake()
    {
        m_playerMask = LayerMask.GetMask("Player");
        m_enemy = GetComponent<Enemy>();
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

    public virtual void onGoalActivated(Action_Base linkedAction)
    {
        LinkedAction = linkedAction;
    }

    public virtual void onGoalDeactivated()
    {
        LinkedAction = null;
    }
}
