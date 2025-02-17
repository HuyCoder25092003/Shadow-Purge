using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum GunType
{
    Assault = 1,
    HandGun = 2,
    ShotGun = 3,
    MachineGun = 4,
    MiniGun = 5

}
public class GunDataIngame
{
    public ConfigWeaponRecord cf;
    public PositionFire positionFire;
}
public abstract class WeaponBehaviour : MonoBehaviour
{
    public GunType gunType;
    public float mul_speed_reload_anim = 1;
    public AnimatorOverrideController overrideController;
    public CharacterDataBinding characterDataBinding;

    public int clip_size = 5;
    public int number_bullet;
    public float rof = 0.2f;
    public float min_accuracy = 20;
    public float max_accuracy = 70;
    public float drop_accuracy = 5;
    public float recovery_accuracy = 0.5f;
    public float accuracy = 50f;
    public float reload_time = 2f;
    public int damage = 10;
    public float range;
    public float force = 100;
    public bool isFire;
    public bool isReloading;
    public float fire_time;
    public UnityEvent<int> OnBulletChange;
    public UnityEvent<float> OnReload;
    public MuzzleFlash muzzleFlash;
    public Transform projecties_pb;
    public string name_bullet_pool;
    public GunDataIngame gunDataIngame;
    public ShellControl shellControl;
    public AudioSource audioSource_;
    public AudioClip[] sfx_fires;
    public AudioClip sfx_ready;
    public AudioClip sfx_reload;

    public IWeaponHandle iWeaponHandle;
    public bool semi_auto = true;
    private bool checkSemiAuto;
    public bool isReloadInput;
    public ConfigWeaponRecord cf;

    public virtual void Setup(GunDataIngame data)
    {
        min_accuracy = data.cf.Accuracy_min;
        max_accuracy = data.cf.Accuracy_max;
        range = data.cf.Range;
        accuracy = min_accuracy;
        this.gunDataIngame = data;
        cf = data.cf;
        rof = cf.ROF;
        reload_time = cf.Reload;
        min_accuracy = cf.Accuracy_min;
        max_accuracy = cf.Accuracy_min;
        damage = cf.Damge;

        characterDataBinding = gameObject.GetComponentInParent<CharacterDataBinding>();
        number_bullet = clip_size;
        BYPool pool = new BYPool(name_bullet_pool, clip_size, projecties_pb);
        BYPoolManager.instance.AddNewPool(pool);

        Init();
    }
    public abstract void Init();

    public void ReadyGun()
    {
        accuracy = min_accuracy;

        gameObject.SetActive(true);
        audioSource_.PlayOneShot(sfx_ready);
        characterDataBinding.SetSpeedReload(mul_speed_reload_anim);

        characterDataBinding.PlayShowGun();

        if (isReloading)
        {
            iWeaponHandle.ReloadHandle();
        }
    }
    public void HideGun()
    {
        gameObject.SetActive(false);
    }
    public void OnFire(bool isFire_)
    {
        this.isFire = isFire_;
        checkSemiAuto = true;
        fire_time = 0;
    }
    private void Update()
    {
        fire_time += Time.deltaTime;
        if (semi_auto)
        {
            if (isFire && !isReloading)
            {
                if (number_bullet > 0 && fire_time >= rof)
                {
                    number_bullet--;
                    fire_time = 0;
                    muzzleFlash.FireHandle();

                    iWeaponHandle.FireHandle();

                    OnBulletChange?.Invoke(number_bullet);


                }
            }
        }
        else
        {

            if (isFire && !isReloading)
            {
                if (number_bullet > 0 && fire_time >= rof && checkSemiAuto)
                {
                    checkSemiAuto = false;
                    number_bullet--;
                    fire_time = 0;
                    muzzleFlash.FireHandle();

                    iWeaponHandle.FireHandle();

                    OnBulletChange?.Invoke(number_bullet);


                }
            }
        }

        accuracy = Mathf.Lerp(accuracy, min_accuracy, Time.deltaTime * recovery_accuracy);
    }

    //IEnumerator SFXProgress()
    //{
    //    yield return new WaitForEndOfFrame();
    //    audioSource_.PlayOneShot(sfx_fires.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
    //}
    public void Reload()
    {
        if (isReloadInput)
            iWeaponHandle.ReloadHandle();
    }

}
