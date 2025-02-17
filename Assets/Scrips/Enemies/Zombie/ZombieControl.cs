using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieControl : EnemyControl
{
    public float speed_move = 2;

    public ZombieDataBinding dataBinding;
    public ZombieAttackState attackState;
    public ZombieChaseState chaseState;
    public ZombieWanderState wanderState;
    public ZomnieStartState startState;
    public ZombieDeadState deadState;
    public float dot_attack = 0.5f;
    public override void Setup(ConfigEnemyRecord configEnemy)
    {
        base.Setup(configEnemy);


        chaseState.parent = this;
        attackState.parent = this;
        wanderState.parent = this;
        startState.parent = this;
        deadState.parent = this;

        agent_.updateRotation = false;
        GotoState(startState);
        StartCoroutine("LoopCheckAttack");
    }
    public void RotateAgent()
    {
        Vector3 dir = agent_.steeringTarget - trans.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            trans.localRotation = Quaternion.Slerp(trans.localRotation, q, Time.deltaTime * 120);
        }

    }
    IEnumerator LoopCheckAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Vector3 target_character = characterControl.trans.position;

            float dis = Vector3.Distance(trans.position, target_character);
            Vector3 dir = target_character - trans.position;
            dir.Normalize();
            float dot = Vector3.Dot(trans.forward, dir);



            if (dis <= range_Detect && dot > dot_attack)
            {
                if (current_state == wanderState || current_state == startState)
                    GotoState(chaseState);
            }    
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void OnDamage(Bulletdata bulletdata)
    {
        if (hp <= 0)
            return;
        this.bulletdata = bulletdata;
        hp = hp - bulletdata.damage;
        
        if (hp <= 0)
        {
            hp = 0;
            if(current_state!=deadState)
            {
                GotoState(deadState);
                Invoke("ImpactPhysic", 0.1f);
            }
        }
        base.OnDamage(bulletdata);
    }
    private void ImpactPhysic()
    {
        bulletdata.rig_body.AddForceAtPosition(bulletdata.force, bulletdata.point_impact, ForceMode.Impulse);
    }
}
