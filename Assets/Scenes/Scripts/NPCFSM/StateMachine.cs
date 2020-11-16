using System;
using Scenes.Scripts.NPCFSM;
using UnityEngine;

[Serializable]
public class StateMachine
{
    [SerializeField] private StateMachine nextStateMachine;
    [SerializeField] private NpcProperties npcProperties;
    public enum CurrentState
    {
    
        Idle, Move, Attack, Hurt, Charge, Dead
    }
    public enum StateProcess
    {
        Enter, Update, Exit
    }

    private StateProcess stateStatus;
    
    

    public virtual void Enter()
    {
        stateStatus = StateProcess.Update;
    }
    public virtual void Update()
    {
        stateStatus = StateProcess.Update; // run update while no condition to exit
    }
    public virtual void Exit()
    {
        stateStatus = StateProcess.Exit;
    }

    public StateMachine Process()
    {
        Debug.Log(stateStatus);
        if (stateStatus == StateProcess.Enter) Enter();
        if (stateStatus == StateProcess.Update) Update();
        if (stateStatus == StateProcess.Exit)
        {
            Exit();
            nextStateMachine.npcProperties = npcProperties;
            return nextStateMachine;
        }
        return this;
    }
    
}
