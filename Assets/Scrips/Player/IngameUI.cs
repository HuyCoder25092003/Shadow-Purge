using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : BySingleton<IngameUI>
{
    public Image iconGun;
    public Text gun_name_lb;
    public Text gun_Ammo_lb;
    public Button btn_click;
    private WeaponBehaviour cur_wp;
    public GameObject reload_ob;
    public Image reload_pg;
    public Joystick joystick;
    public Transform trans_fire;
    public GameObject btn_reload;
    public TMP_Text hp_lb;
    public Image hp_progress;
    public RectTransform parent_Hub;
    public RectTransform parent_indicator;
    public TMP_Text wave_lb;
    public CanvasGroup wait_loading;
    public Transform wait_anim;
    IEnumerator Start()
    {
        wait_loading.alpha = 1;
        wait_anim.DOLocalRotate(new Vector3(0, 0, -360), 2,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1,LoopType.Incremental) ;
        WeaponControl.instance.OnChangeGun.AddListener(OnChangeGun);
        InputManager.instance.joystick = joystick;
        yield return new WaitForSeconds(0.2f);
     
        MissionManager.instance.OnWaveChange.AddListener((c_wave, total) =>
        {
            wave_lb.text = $"{c_wave}/{total}";
        });
    }
    
    private void OnChangeGun(WeaponBehaviour wp)
    {
        if(wait_loading.alpha>0)
        {
            wait_anim.DOKill();
            wait_loading.DOFade(0, 1);
        }
        reload_ob.SetActive(false);
        StopCoroutine("ReloadProgress");
        if (cur_wp != null)
        {
            cur_wp.OnBulletChange.RemoveListener(OnBulletChange);
            cur_wp.OnReload.RemoveListener(Cur_wp_OnReload);

        }
        cur_wp = wp;
        btn_reload.SetActive(cur_wp.isReloadInput);
        cur_wp.OnBulletChange.AddListener(OnBulletChange);
        cur_wp.OnReload.AddListener(Cur_wp_OnReload);
        gun_Ammo_lb.text = $"{wp.number_bullet}";
        iconGun.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(wp.cf.Prefab);
        gun_name_lb.text = wp.cf.Name;
    }

    private void Cur_wp_OnReload(float obj)
    {
        reload_ob.SetActive(true);
        StopCoroutine("ReloadProgress");
        StartCoroutine("ReloadProgress", obj);
    }
    IEnumerator ReloadProgress(float reloadTime)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        float count_time = 0;
        while (count_time < reloadTime)
        {
            yield return wait;
            count_time += 0.1f;
            reload_pg.fillAmount = count_time / reloadTime;

        }
        reload_ob.SetActive(false);
    }

    private void OnBulletChange(int obj)
    {
        gun_Ammo_lb.text = $"{obj}";
    }
    public void OnHP_PlayerChange(int hp,int maxhp)
    {
        hp_lb.text = $"{hp}/{maxhp}";

        float val = (float)hp / (float)maxhp;
        float x = val * 980f;
        hp_progress.rectTransform.sizeDelta = new Vector2(x, 50);
    }
    public void OnChangeGun()
    {
        InputManager.instance.SetChangeGun();
    }
    public void OnReload()
    {
        InputManager.instance.SetReload();
    }
    public void OnFire(bool isFire)
    {
        float scale = isFire ? 0.8f : 1f;
        trans_fire.localScale = Vector3.one * scale;
        InputManager.instance.SetFire(isFire);
    }
    public void OnBack()
    {
        DialogManager.instance.ShowDialog(DialogIndex.PauseDialog);
    }
}
