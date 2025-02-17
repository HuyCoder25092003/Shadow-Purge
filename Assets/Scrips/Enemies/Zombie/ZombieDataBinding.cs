using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDataBinding : MonoBehaviour
{
    public Animator animator;
    public float Speed
    {
        set
        {
            animator.SetFloat(Anim_Key_Speed, value);
        }
    }
   public bool Attack
    {
        set
        {
            animator.Play("Attack", 0, 0);
        }
    }
    public bool Dead
    {
        set
        {
            if(value)
            {
                puppetMaster.mode = PuppetMaster.Mode.Active;
                puppetMaster.Kill(stateSettings);
            }
        }
    }
    private int Anim_Key_Speed;
    public PuppetMaster puppetMaster;
    public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;
    void Awake()
    {
       
        Anim_Key_Speed = Animator.StringToHash("Speed");
    }

   
}
