using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DialogIndex
{
    SettingDialog = 0,
    WeaponInfoDialog = 1,
    MessageDialog = 2,
    ShopDialog=3,
    EquipWeaponDialog=4,
    MissionDetailDialog=5,
    PauseDialog=6,
    WinDialog=7
}
public class DialogParam
{

}
public class MessageDialogParam : ViewParam
{
    public string mess;
}
public class EquipWeaponDialogParam: DialogParam
{
    public int id_new_wp;
         
}
public class MissionDetailDialogParam : DialogParam
{
    public MissionViewListData data;

}
public class WinDialogParam : DialogParam
{
    public ConfigMissionRecord config_ms;
    public bool isNew_1;
    public bool star_1;
    public bool isNew_2;
    public bool star_2;
    public bool isNew_3;
    public bool star_3;
}
public class DialogConfig
{
    public static DialogIndex[] dialogIndices = {

        DialogIndex.SettingDialog,
        DialogIndex.WeaponInfoDialog,
        DialogIndex.MessageDialog,
        DialogIndex.ShopDialog,
        DialogIndex.EquipWeaponDialog,
        DialogIndex.MissionDetailDialog,
        DialogIndex.PauseDialog,
        DialogIndex.WinDialog
    };

}
