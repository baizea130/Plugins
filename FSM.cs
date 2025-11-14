using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,
    Move,
    BeAtked
}
public interface IState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}
[Serializable]
public class BlackBoard{}

public class FSM
{
    public StateType CurrentState;
    public Dictionary<StateType, IState> stateDict;
    public BlackBoard board;
    public FSM(BlackBoard board)
    {
        stateDict = new Dictionary<StateType, IState>();
        this.board = board;
    }
    public void AddState(StateType type, IState state)
    {
        if (stateDict.ContainsKey(type))
        {
            return;
        }
        stateDict.Add(type, state);
    }
    public void SwitchState(StateType type)
    {
        if (!stateDict.ContainsKey(type))
        {
            return;
        }
        stateDict[CurrentState].ExitState();
        CurrentState = type;
        stateDict[CurrentState].EnterState();
    }
    public void OnUpdate()
    {
        stateDict[CurrentState].UpdateState();
    }
}
