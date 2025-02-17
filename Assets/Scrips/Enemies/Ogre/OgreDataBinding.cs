using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreDataBinding : MonoBehaviour
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
            if (value)
            {
                animator.SetTrigger(Anim_Key_Attack);

            }
        }
    }
    private int Anim_Key_Speed;
    private int Anim_Key_Attack;
    void Start()
    {
        Anim_Key_Speed = Animator.StringToHash("Speed");
        Anim_Key_Attack = Animator.StringToHash("Attack");
    }
}
