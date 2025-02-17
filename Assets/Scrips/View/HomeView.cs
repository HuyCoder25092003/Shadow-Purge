using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public TMP_Text cash_lb;
    public Image icon_gun_1;
    public TMP_Text gun_name_1;

    public Image icon_gun_2;
    public TMP_Text gun_name_2;
    public override void Setup(ViewParam data)
    {
        int cash = DataAPIController.instance.GetCash();
        cash_lb.text = $"{cash}";
        SetGunEquipInfo();
    }
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataPath.CASH, OnCashChange);
        DataTrigger.RegisterValueChange(DataPath.INFO, OnWeaponChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnCashChange);
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnWeaponChange);
    }
    private void OnCashChange(object dataChange)
    {
        cash_lb.text = $"{((int)dataChange)}";
    }
    private void SetGunEquipInfo()
    {
        UserInfo userInfo = DataAPIController.instance.GetUserInfo();
        List<int> gun_ids = userInfo.guns_equip;
        WeaponData weaponData_1 = DataAPIController.instance.GetWeaponDataById(gun_ids[0]);

        ConfigWeaponRecord cf_gun_1 = ConfigManager.instance.configWeapon.GetRecordByKeySearch(weaponData_1.id, weaponData_1.level);
        icon_gun_1.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_gun_1.Prefab);
        gun_name_1.text = cf_gun_1.Name;

        WeaponData weaponData_2 = DataAPIController.instance.GetWeaponDataById(gun_ids[1]);
        ConfigWeaponRecord cf_gun_2 = ConfigManager.instance.configWeapon.GetRecordByKeySearch(weaponData_2.id, weaponData_2.level);
        icon_gun_2.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName(cf_gun_2.Prefab);
        gun_name_2.text = cf_gun_2.Name;
    }
    private void OnWeaponChange(object dataChange)
    {
        SetGunEquipInfo();
    }
    public void OnAddCash()
    {
        DataAPIController.instance.AddCash(50, () =>
        {
            Debug.LogError(" cash add");
        });
    }
    public void OnWeaponView()
    {
        WeaponViewParam weaponViewParam = new WeaponViewParam();
        weaponViewParam.weaponID = 2;
        ViewManager.instance.SwitchView(ViewIndex.WeaponView, weaponViewParam);
    }
  
    public void OnSwapGun()
    {
        DataAPIController.instance.OnSwapGun();
    }

   public void Setting()
    {
        DialogManager.instance.ShowDialog(DialogIndex.SettingDialog);
    }
    public void OnShop()
    {
        DialogManager.instance.ShowDialog(DialogIndex.ShopDialog);
    }
    public void OnMission()
    {
        ViewManager.instance.SwitchView(ViewIndex.MissionView);
    }
}
