using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZombieWanderState : FSM_State
{
    [NonSerialized]
    public ZombieControl parent;

    private float time_Delay;
    private Vector3 cur_point;
    private Vector3 GetPoint()
    {
        time_Delay = 0;
        Vector2 p = UnityEngine.Random.insideUnitCircle * UnityEngine.Random.Range(2f, 10f);
        return parent.trans.position + new Vector3(p.x, 0, p.y);
    }

    public override void OnEnter()
    {
        cur_point = GetPoint();
        parent.agent_.enabled = true;
        parent.agent_.Warp(parent.trans.position);
        parent.agent_.isStopped = false;
        parent.agent_.speed = 1;
    }
    public override void FixedUpdateState()
    {
        time_Delay += Time.deltaTime;
        parent.agent_.SetDestination(cur_point);
        float dis = Vector3.Distance(parent.trans.position, cur_point);
        if (parent.agent_.remainingDistance <= 0.1f&&time_Delay>0.5f)
        {
            cur_point = GetPoint();
        }
        parent.RotateAgent();
 
        parent.dataBinding.Speed = parent.agent_.velocity.magnitude;
     


    }
  
    public override void Exit()
    {
        base.Exit();
        parent.dataBinding.Speed = 0;
        parent.agent_.isStopped = true;
    }
}
