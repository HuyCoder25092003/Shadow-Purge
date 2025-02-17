using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_System : MonoBehaviour
{
    // Start is called before the first frame update
    public FSM_State current_state;
    public void GotoState(FSM_State newSate)
    {
        if(current_state!=null)
        {
            current_state.Exit();
        }
        current_state = newSate;
        current_state.OnEnter();
    }
    public void GotoState(FSM_State newSate,object data)
    {
        current_state?.Exit();

        current_state = newSate;
        current_state.OnEnter(data);
    }
    public void OnAnimationEnter()
    {
        current_state?.OnAnimationEnter();
    }
    public void OnAnimationMiddle()
    {
        current_state?.OnAnimationMiddle();
    }
    public void OnAnimationExit()
    {
        current_state?.OnAnimationExit();
    }
    // Update is called once per frame
    public virtual void Update()
    {
        current_state?.UpdateState();
    }
    public virtual void LateUpdate()
    {
        current_state?.LateUpdateState();
    }
    public virtual void FixedUpdate()
    {
        current_state?.FixedUpdateState();
    }
}
