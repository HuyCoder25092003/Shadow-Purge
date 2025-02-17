using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class OgreControl : EnemyControl
{
    public float armo = 1;
    public float speed_rotate = 5;
    public Ogre_AttackState attackState;
    public Ogre_ChaseState chaseState;
    public Ogre_WanderState wanderState;
    public OgreDataBinding dataBinding;
    public override void Setup(ConfigEnemyRecord configEnemy)
    {
        base.Setup(configEnemy);
        Debug.LogError(" Ogre Init");
        hp = hp * 2;
        attackState.parent = this;
        chaseState.parent = this;
        wanderState.parent = this;
        GotoState(wanderState);
        agent_.Warp(trans.position);
        agent_.updateRotation = false;
    }
}
