using SWS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieDeadState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;
    public override void OnEnter()
    {
        base.OnEnter();
        parent.dataBinding.Dead = true;
        parent.OnDead();
    }
   
}
