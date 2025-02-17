using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipWeaponDialog : BaseDialog
{
    public Image icon_gun_slot_1;
    public TMP_Text name_gun_slot_1;

    public Image icon_gun_slot_2;
    public TMP_Text name_gun_slot_2;

    private EquipWeaponDialogParam d_param;
    public override void Setup(DialogParam data)
    {
        d_param = data as EquipWeaponDialogParam;
        SetGunEquipInfo();
    }
    private void SetGunEquipInfo()
    {
        UserInfo userInfo = DataAPIController.instance.GetUserInfo();
        List<int> gun_ids = userInfo.guns_equip;
        WeaponData weaponData_1 = DataAPIController.instance.GetWeaponDataById(gun_ids[0]);

        ConfigWeaponRecord cf_gun_1 = ConfigManager.instance.configWeapon.GetRecordByKeySearch(weaponData_1.id, weaponData_1.level);
        icon_gun_slot_1.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_gun_1.Prefab);
        name_gun_slot_1.text = cf_gun_1.Name;

        WeaponData weaponData_2 = DataAPIController.instance.GetWeaponDataById(gun_ids[1]);
        ConfigWeaponRecord cf_gun_2 = ConfigManager.instance.configWeapon.GetRecordByKeySearch(weaponData_2.id, weaponData_2.level);
        icon_gun_slot_2.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_gun_2.Prefab);
        name_gun_slot_2.text = cf_gun_2.Name;
    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(this.dialogIndex);
    }
    public void OnSelect_Slot_1()
    {
        DataAPIController.instance.OnEquip(d_param.id_new_wp, 1);
        DialogManager.instance.HideDialog(this.dialogIndex);
    }
    public void OnSelect_Slot_2()
    {
        DataAPIController.instance.OnEquip(d_param.id_new_wp, 2);
        DialogManager.instance.HideDialog(this.dialogIndex);

    }
}
