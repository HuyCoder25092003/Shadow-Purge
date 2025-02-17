using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ogre_ChaseState : FSM_State
{
    [NonSerialized]
    public OgreControl parent;
    public float speed_move = 2;

    public override void OnEnter()
    {
        parent.agent_.isStopped = false;
        parent.time_delay_agent = 0;
        parent.agent_.speed = 2;
        base.OnEnter();
        Debug.LogError("Ogre_ChaseState enter ");
    }
    public override void UpdateState()
    {
        float dis = Vector3.Distance(parent.trans.position, parent.characterControl.trans.position);

        if (dis <= parent.range_Attack)
        {
            parent.GotoState(parent.attackState);
        }
        else if (dis > parent.range_Detect)
        {
            parent.GotoState(parent.wanderState);
        }
        else
        {
            if (parent.isMoveAgent)
            {
                parent.agent_.SetDestination(parent.characterControl.trans.position);

                parent.dataBinding.Speed = parent.agent_.velocity.magnitude;
                RotateAgent();
                if (!parent.agent_.hasPath)
                {
                    parent.GotoState(parent.wanderState);
                }

            }
            else
            {
                Vector3 dir = parent.characterControl.trans.position - parent.trans.position;
                dir.Normalize();
                if (dir != Vector3.zero)
                {
                    Quaternion q = Quaternion.LookRotation(dir.normalized, Vector3.up);
                    parent.trans.localRotation = Quaternion.Slerp(parent.trans.localRotation, q, Time.deltaTime * 2);

                }
                parent.trans.Translate(Vector3.forward * Time.deltaTime * speed_move);
                parent.dataBinding.Speed = 2;
            }


        }
    }
    private void RotateAgent()
    {
        Vector3 dir = parent.agent_.steeringTarget - parent.trans.position;
        dir.Normalize();
        if (dir.magnitude > 0)// dir!=vector3.zeor
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            parent.trans.localRotation = Quaternion.Slerp(parent.trans.localRotation, q, Time.deltaTime * 20);
        }

    }
    public override void Exit()
    {
        base.Exit();
        parent.dataBinding.Speed = 0;
        parent.agent_.isStopped = true;
    }
}
