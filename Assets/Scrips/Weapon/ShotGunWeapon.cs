using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
public class ShotGunWeapon : WeaponBehaviour
{
    public int bps = 1;
    public override void Init()
    {
        iWeaponHandle = new IShotGun();
        iWeaponHandle.Init(this);
    }
    public override void Setup(GunDataIngame data)
    {
        base.Setup(data);
      
    }
    public void StartReload()
    {
        StopCoroutine("ReloadProgress");
        StartCoroutine("ReloadProgress");
    }

    IEnumerator ReloadProgress()
    {
        yield return new WaitForSeconds(2.0f);
        isReloading = true;
        while (number_bullet < clip_size)
        {
            audioSource_.PlayOneShot(sfx_reload);
            OnReload?.Invoke(reload_time);
            characterDataBinding.PlayReloadGun();
            yield return new WaitForSeconds(reload_time);
           
            number_bullet ++;
            OnBulletChange?.Invoke(number_bullet);
        }
        isReloading = false;

    }
}
public class IShotGun : IWeaponHandle
{
    ShotGunWeapon wp;
    public void FireHandle()
    {
        
        // throw new System.NotImplementedException();
        wp.characterDataBinding.PlayFireGun();
        wp.StopCoroutine("ReloadProgress");
        CreateBullet();
        wp.isReloading = false;
        if (wp.number_bullet <wp.clip_size)
        {
            ReloadHandle();
        }
    }
    private void CreateBullet()
    {

        wp.accuracy += wp.drop_accuracy;
        wp.accuracy = Mathf.Clamp(wp.accuracy, wp.min_accuracy, wp.max_accuracy);

        wp.shellControl.Fire();

        wp.audioSource_.PlayOneShot(wp.sfx_fires.OrderBy(x => Guid.NewGuid()).FirstOrDefault());
        // AudioClip sfx = sfx_fires[UnityEngine.Random.Range(0, sfx_fires.Length)];
        //audioSource_.PlayOneShot(sfx);
        // StartCoroutine("SFXProgress");
        for(int i=0;i<wp.bps;i++)
        {
            Transform bl = BYPoolManager.instance.dic_pool[wp.name_bullet_pool].Spawned();

            bl.position = wp.gunDataIngame.positionFire.GetPosFire(out Vector3 dir);
            float accuracy_val = wp.accuracy * 0.08f;
            float x = UnityEngine.Random.Range(-accuracy_val, accuracy_val);
            float y = UnityEngine.Random.Range(-accuracy_val, accuracy_val);
            Quaternion q = Quaternion.Euler(x, y, 0);
            bl.forward = q * dir;

            BulletControl bl_control = bl.GetComponent<BulletControl>();
            Bulletdata bulletdata = new Bulletdata { damage = 2, name_pool = wp.name_bullet_pool, cf_wp = wp.cf };
            bulletdata.force = wp.force * bl.forward;
            bl_control.Setup(bulletdata);
        }
       
    }
    public void Init(WeaponBehaviour wp)
    {
        this.wp = (ShotGunWeapon)wp;
        // new System.NotImplementedException();
    }

    public void ReloadHandle()
    {
        //throw new System.NotImplementedException();
        wp.StartReload();
    }
}