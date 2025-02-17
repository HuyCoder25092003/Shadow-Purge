using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewDetail : MonoBehaviour
{
    public Image iconGun;
    public TMP_Text name_lb;
    public GameObject price_object;
    public TMP_Text price_lb;
    public TMP_Text level_lb;
    public TMP_Text slot_lb;
    // detail 
    public Vector2 ogrinal_size_dl;
    public TMP_Text damage_val_;
    public Image damage_process;
    public Image damage_process_next;
    public TMP_Text range_val_;
    public Image range_process;
    public Image range_process_next;
    public TMP_Text accuracy_val_;
    public Image accuracy_process;
    public Image accuracy_process_next;
    public TMP_Text rof_val_;
    public TMP_Text reload_val_;

    public Button btn_equip;
    public Button btn_Upgrade;
    public Button btn_buy;
    public GameObject max_level;
    private ConfigWeaponRecord cf_weapon;
    private ConfigWeaponRecord cf_weapon_level_next;
    private WeaponData wp_data;
    private int cash = 0;
    private bool isRegisterCash = false;
    public void Setup(WeaponViewGunListItem wp_list_item)
    {
        cash = DataAPIController.instance.GetCash();
        if (!isRegisterCash)
        {
            DataTrigger.RegisterValueChange(DataPath.CASH, (data_change) =>
            {
                cash = DataAPIController.instance.GetCash();
                SetInfo();
            });
            DataTrigger.RegisterValueChange(DataPath.INFO, (info_Change) =>
            {
                SetInfo();
            });
            isRegisterCash = true;
        }


        cf_weapon = wp_list_item.data.cf;
        wp_data = wp_list_item.wp_data;
        SetInfo();
    }
    private void SetInfo()
    {
        iconGun.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_weapon.Prefab);
        name_lb.text = cf_weapon.Name;
        slot_lb.text = string.Empty;
        level_lb.text = string.Empty;
        btn_buy.gameObject.SetActive(false);
        btn_Upgrade.gameObject.SetActive(false);
        btn_equip.gameObject.SetActive(false);
        price_object.SetActive(false);
        max_level.SetActive(false);
        damage_process_next.gameObject.SetActive(false);
        range_process_next.gameObject.SetActive(false);
        accuracy_process_next.gameObject.SetActive(false);
        string s_rof_next = string.Empty;
        int num_max_level = ConfigManager.instance.configWeapon.GetMaxLevel(cf_weapon.id);
        if (wp_data != null)
        {
            cf_weapon = ConfigManager.instance.configWeapon.GetRecordByKeySearch(cf_weapon.id, wp_data.level);
            if (wp_data.level >= num_max_level)
            {
                max_level.SetActive(true);
            }
            else
            {
                price_object.SetActive(true);
                level_lb.text = $"Level {wp_data.level}";
                cf_weapon_level_next = ConfigManager.instance.configWeapon.GetRecordByKeySearch(cf_weapon.id, wp_data.level + 1);
                price_lb.text = $"{cf_weapon_level_next.Price}";
                btn_Upgrade.gameObject.SetActive(true);
                // check cash 
                btn_Upgrade.interactable = cash >= cf_weapon_level_next.Price;
                // set next level 
                damage_process_next.gameObject.SetActive(true);
                range_process_next.gameObject.SetActive(true);
                accuracy_process_next.gameObject.SetActive(true);

                float time_anim_next = 0.5f;
                // damage

                float val_damage_n = (float)cf_weapon_level_next.Damge / (float)ConfigManager.instance.configDefault.max_damage;
                damage_process_next.rectTransform.DOSizeDelta(new Vector2(val_damage_n * ogrinal_size_dl.x, ogrinal_size_dl.y), time_anim_next);
                // range

                float val_range_n = (float)cf_weapon_level_next.Range / (float)ConfigManager.instance.configDefault.max_range;

                range_process_next.rectTransform.DOSizeDelta(new Vector2(val_range_n * ogrinal_size_dl.x, ogrinal_size_dl.y), time_anim_next);
                //Accuracy

                int acc_next = cf_weapon_level_next.Accuracy_max - cf_weapon_level_next.Accuracy_min;

                // 100 <=> 400
                accuracy_process_next.rectTransform.DOSizeDelta(new Vector2(acc_next * ogrinal_size_dl.x * 0.01f, ogrinal_size_dl.y), time_anim_next);
                accuracy_process_next.rectTransform.DOAnchorPos(new Vector2(cf_weapon_level_next.Accuracy_min * ogrinal_size_dl.x * 0.01f, 0), time_anim_next);

                s_rof_next = $"-><color=#00ffffff>{cf_weapon_level_next.ROF}</color>";

            }

            int index = -1;

            if (DataAPIController.instance.CheckWeaponEquip(cf_weapon.id, out index))
            {
                slot_lb.text = $"SLOT {index}";
            }
            else
            {
                slot_lb.text = string.Empty;
                btn_equip.gameObject.SetActive(true);
            }
        }
        else
        {
            cf_weapon = ConfigManager.instance.configWeapon.GetRecordByKeySearch(cf_weapon.id, 1);
            price_lb.text = $"{cf_weapon.Price}";
            price_object.SetActive(true);
            btn_buy.gameObject.SetActive(true);
            btn_buy.interactable = cash >= cf_weapon.Price;
        }
        float time_anim = 0.5f;

        damage_val_.text = cf_weapon.Damge.ToString();
        float val_damage = (float)cf_weapon.Damge / (float)ConfigManager.instance.configDefault.max_damage;
        damage_process.rectTransform.DOSizeDelta(new Vector2(val_damage * ogrinal_size_dl.x, ogrinal_size_dl.y), time_anim);

        range_val_.text = cf_weapon.Range.ToString();
        float val_range = (float)cf_weapon.Range / (float)ConfigManager.instance.configDefault.max_range;

        range_process.rectTransform.DOSizeDelta(new Vector2(val_range * ogrinal_size_dl.x, ogrinal_size_dl.y), time_anim);

        accuracy_val_.text = cf_weapon.Accuracy_min.ToString() + "-" + cf_weapon.Accuracy_max.ToString();
        int acc = cf_weapon.Accuracy_max - cf_weapon.Accuracy_min;


        accuracy_process.rectTransform.DOSizeDelta(new Vector2(acc * ogrinal_size_dl.x * 0.01f, ogrinal_size_dl.y), time_anim);
        accuracy_process.rectTransform.DOAnchorPos(new Vector2(cf_weapon.Accuracy_min * ogrinal_size_dl.x * 0.01f, 0), time_anim);

        rof_val_.text = $"{cf_weapon.ROF}" + s_rof_next;

        reload_val_.text = $"{cf_weapon.Reload}";
    }
    public void OnEquip()
    {
        EquipWeaponDialogParam param = new EquipWeaponDialogParam();
        param.id_new_wp = cf_weapon.id;
        DialogManager.instance.ShowDialog(DialogIndex.EquipWeaponDialog,param);
    }
    public void OnUpgrade()
    {
        DataAPIController.instance.OnUpgrade(wp_data, (res) =>
        {
            if(res!=null)
            {
                wp_data = res;
                SetInfo();
            }
            else
            {
                // show log 
            }
        });
    }
    public void OnBuy()
    {
        DataAPIController.instance.BuyGun(cf_weapon.id, (res) =>
        {
            if (res != null)
            {
                wp_data = res;
                SetInfo();

            }
            else
            {
                // show log 
            }
        });
    }
}
