using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponViewGunListItem : EnhancedScrollerCellView
{
    public Image iconGun;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public TMP_Text slot_lb;
    public WeaponData wp_data;
    public GameObject lock_object;
    public GameObject Select_object;
    public WeaponView weaponView;
    public WeaponListData data;
    private int id_Register_data;
   
    // Start is called before the first frame update
    public void SetDataCell(WeaponListData data)
    {
        if(id_Register_data!=0)
        {
            DataTrigger.UnRegisterValueChange(DataPath.INFO, InfoChange);
            DataTrigger.UnRegisterValueChange(DataPath.DIC_WEAPON + "/K_" + id_Register_data, DataWPChange);
            id_Register_data = 0;
        }
        this.data = data;
        this.weaponView = data.weaponView;
        Select_object.SetActive(false);
        iconGun.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(data.cf.Prefab);
        name_lb.text = data.cf.Name;
        slot_lb.text = string.Empty;
        level_lb.text = string.Empty;
        wp_data = DataAPIController.instance.GetWeaponDataById(data.cf.id);
        if(wp_data!=null)
        {  
            level_lb.text =$"Level {wp_data.level}";
            int index = -1;
            if(DataAPIController.instance.CheckWeaponEquip(data.cf.id,out index))
            {
                slot_lb.text = $"SLOT {index}";
            }
            id_Register_data = data.cf.id;
            DataTrigger.RegisterValueChange(DataPath.INFO, InfoChange);
            DataTrigger.RegisterValueChange(DataPath.DIC_WEAPON + "/K_" + id_Register_data, DataWPChange);
        }
        lock_object.SetActive(wp_data == null);
        
    }
    private void InfoChange(object data_change)
    {
        int index = -1;
        if (DataAPIController.instance.CheckWeaponEquip(data.cf.id, out index))
        {
            slot_lb.text = $"SLOT {index}";
        }
        else
        {
            slot_lb.text = string.Empty;
        }

    }
    private void DataWPChange(object data_change)
    {
        this.wp_data = (WeaponData)data_change;

        level_lb.text = $"Level {wp_data.level}";
        int index = -1;
        if (DataAPIController.instance.CheckWeaponEquip(data.cf.id, out index))
        {
            slot_lb.text = $"SLOT {index}";
        }
    }
    public void OnClick()
    {
        if (weaponView.Cur_wp_item_list == this)
            return;
        Select_object.SetActive(true);
        weaponView.OnWeaponSelecte(this);
    }
    public void OnDeSelect()
    {
        Select_object.SetActive(false);
    }
    public override void RefreshCellView()
    {
        if (cellIndex == 0)
        {
            OnClick();
        }
    }
}
