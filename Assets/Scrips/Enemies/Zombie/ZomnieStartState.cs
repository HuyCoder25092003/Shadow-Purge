using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZomnieStartState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    public override void OnEnter()
    {
        base.OnEnter();
        parent.splineMove_.StartMove();
        parent.dataBinding.Speed = 1;
        parent.splineMove_.movementEndEvent += MovementEndEvent;
        parent.splineMove_.movementChange.RemoveAllListeners();
        parent.splineMove_.movementChange.AddListener(OnPointChange);
    }
    private void OnPointChange(int index)
    {
       // Debug.LogError(" index move : " + index);
    }
    private void MovementEndEvent()
    {
        parent.GotoState(parent.wanderState);
    }

    public override void LateUpdateState()
    {
        base.LateUpdateState();
        
    }
    public override void Exit()
    {
        base.Exit();
        parent.splineMove_.movementEndEvent -= MovementEndEvent;
        parent.splineMove_.movementChange.RemoveAllListeners();
        parent.splineMove_.Stop();
    }
}
