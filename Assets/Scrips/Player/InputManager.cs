using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : BySingleton<InputManager>
{
    public static Vector3 move_Dir;
    public Joystick joystick;
    public UnityEvent<bool> OnFire;
    public UnityEvent OnReload;
    public UnityEvent OnChangeGun;
    public void SetFire(bool isFire)
    {
        OnFire?.Invoke(isFire);
    }
    public void SetReload()
    {
        OnReload?.Invoke();
    }
    public void SetChangeGun()
    {
        OnChangeGun?.Invoke();
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move_Dir = new Vector3(x, 0, z);
       
        Vector3 move_dir_js = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        move_Dir += move_dir_js;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnFire?.Invoke(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            OnFire?.Invoke(false);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            OnChangeGun?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReload?.Invoke();
        }
    }
   
}
