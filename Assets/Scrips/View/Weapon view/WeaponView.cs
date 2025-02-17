using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : BaseView
{
    public TMP_Text cash_lb;
    public Text weapon_lv;
    public Text weapon_name;
    private ConfigWeaponRecord cf;
    private WeaponData weaponData;
    public TapWeaponButton tap_buttons;
    public WeaponViewGunListItem Cur_wp_item_list;
    [SerializeField]
    private WeaponViewDetail wp_detail;
    public override void Setup(ViewParam data)
    {
        int cash = DataAPIController.instance.GetCash();
        cash_lb.text = $"{cash}";

        WeaponViewParam param = data as WeaponViewParam;
        weaponData = DataAPIController.instance.GetWeaponDataById(param.weaponID);
        cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(param.weaponID,weaponData.level);
        tap_buttons.Init();
    }
    public override void OnShowView()
    {
        DataTrigger.RegisterValueChange(DataPath.DIC_WEAPON + "/" + cf.id.Tokey(), OnWeaponDataChange);
        DataTrigger.RegisterValueChange(DataPath.CASH, OnCashChange);
    }
    public override void OnHideView()
    {
        DataTrigger.UnRegisterValueChange(DataPath.DIC_WEAPON + "/" + cf.id.Tokey(), OnWeaponDataChange);
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnCashChange);
    }
    private void OnCashChange(object dataChange)
    {
        cash_lb.text = $"{((int)dataChange)}";    
    }
    private void OnWeaponDataChange(object data)
    {
        weaponData = data as WeaponData;

        weapon_lv.text = $"{weaponData.level}";
    }
    public void OnHomeView()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
    public void UpgradeWP()
    {
        DataAPIController.instance.UpgradelevelWeaponDataById(weaponData.id);
    }
    public void OnTab()
    {
        Cur_wp_item_list = null;
    }
    public void OnWeaponSelecte(WeaponViewGunListItem wp_item_list)
    {
        Cur_wp_item_list?.OnDeSelect();
        Cur_wp_item_list = wp_item_list;
        wp_detail.Setup(wp_item_list);
    }
    public void OnShop()
    {
        DialogManager.instance.ShowDialog(DialogIndex.ShopDialog);
    }
}
