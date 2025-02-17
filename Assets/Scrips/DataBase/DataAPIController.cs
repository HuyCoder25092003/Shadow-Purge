using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataAPIController", menuName = "BY/DataAPIController", order = 1)]
public class DataAPIController : ScriptableObject
{
    public static DataAPIController instance;
    [SerializeField]
    private DataModel dataModel;
    public void InitData(Action callback)
    {
        instance = this;
        dataModel.LoadDataLocal((isNew) =>
        {
            Debug.LogError(" isnew : " + isNew);
            callback?.Invoke();

        });
    }
    public int GetCash()
    {
        return dataModel.ReadData<int>(DataPath.CASH);
    }
    public void AddCash(int cash, Action callback)
    {
        int num = dataModel.ReadData<int>(DataPath.CASH);
        num += cash;
        dataModel.UpdateData(DataPath.CASH, num, callback);
    }
    public WeaponData GetWeaponDataById(int id)
    {
        WeaponData wp = dataModel.ReadDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey());
        return wp;
    }
    public void UpgradelevelWeaponDataById(int id)
    {
        int num = dataModel.ReadData<int>(DataPath.CASH);
        if (num >= 30)
        {
            num -= 30;
            dataModel.UpdateData(DataPath.CASH, num, null);

            WeaponData wp = dataModel.ReadDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey());
            wp.level++;
            dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, id.Tokey(), wp, null);
        }


    }
    public List<int> GetWeaponsEquip()
    {
        return dataModel.ReadData<List<int>>(DataPath.GUNS_EQUIP);
    }
    public UserInfo GetUserInfo()
    {
        return dataModel.ReadData<UserInfo>(DataPath.INFO);
    }
    public bool CheckWeaponEquip(int id, out int index_)
    {
        UserInfo info = dataModel.ReadData<UserInfo>(DataPath.INFO);

        int i = info.guns_equip.IndexOf(id);
        if (i < 0)
        {
            index_ = -1;
            return false;
        }
        else
        {
            index_ = i + 1;
            return true;
        }
    }
    public void OnSwapGun()
    {
        UserInfo userInfo = GetUserInfo();
        int gun_1 = userInfo.guns_equip[0];
        int gun_2 = userInfo.guns_equip[1];
        userInfo.guns_equip[0] = gun_2;
        userInfo.guns_equip[1] = gun_1;

        dataModel.UpdateData(DataPath.INFO, userInfo);
    }
    public void OnBuyShop(int id)
    {
        ConfigShopRecord cf = ConfigManager.instance.configShop.GetRecordByKeySearch(id);

        int num = dataModel.ReadData<int>(DataPath.CASH);
        num += cf.Value;
        dataModel.UpdateData(DataPath.CASH, num, null);

    }
    public void OnEquip(int id_wp, int slot)
    {
        UserInfo userInfo = GetUserInfo();
        if (slot == 1)
            userInfo.guns_equip[0] = id_wp;
        if (slot == 2)
            userInfo.guns_equip[1] = id_wp;

        dataModel.UpdateData(DataPath.INFO, userInfo);
    }
    public void BuyGun(int id_wp, Action<WeaponData> callback)
    {
        ConfigWeaponRecord cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(id_wp, 1);
        int cash = GetCash();
        if (cf.Price <= cash)
        {
            WeaponData data = new WeaponData();
            data.id = id_wp;
            data.level = 1;
            dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, data.id.Tokey(), data);
            cash -= cf.Price;
            dataModel.UpdateData(DataPath.CASH, cash);
            callback(data);
        }
        else
        {
            callback(null);
        }
    }
    public void OnUpgrade(WeaponData data, Action<WeaponData> callback)
    {
        ConfigWeaponRecord cf_next_level = ConfigManager.instance.configWeapon.GetRecordByKeySearch(data.id, data.level);
        int cash = GetCash();
        if (cf_next_level != null)
        {
            if (cf_next_level.Price <= cash)
            {
                data.level++;
                dataModel.UpdateDataDictionary<WeaponData>(DataPath.DIC_WEAPON, data.id.Tokey(), data);
                cash -= cf_next_level.Price;
                dataModel.UpdateData(DataPath.CASH, cash);
                callback(data);
            }
            else
            {
                callback(null);
            }
        }
        else
        {
            callback(null);
        }

    }

    public Dictionary<string, MissionData> GetMissionData()
    {
        return dataModel.ReadData<Dictionary<string, MissionData>>(DataPath.DIC_MISSION);
    }
    public MissionData GetMissionDataID(int id)
    {
        return dataModel.ReadDataDictionary< MissionData>(DataPath.DIC_MISSION, id.Tokey());
    }
    public void UpdateMissionWin(WinDialogParam param)
    {
        bool isNewMission = true;
        //
        MissionData missionData = GetMissionDataID(param.config_ms.id);
        if (missionData.star_1 == false)
        {
            if(param.star_1)
            {
                ConfigQuestMissionRecord quest_1 = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(param.config_ms.Quest_1);
                AddCash(quest_1.Reward,null);
                missionData.star_1 = true;
            }


        }
        else
        {
            isNewMission = false;
        }
     
        if (missionData.star_2 == false)
        {
            if (param.star_2)
            {
                ConfigQuestMissionRecord quest_2 = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(param.config_ms.Quest_2);
                AddCash(quest_2.Reward, null);
                missionData.star_2 = true;
            }

        }
        else
        {
            isNewMission = false;
        }

        if (missionData.star_3 == false)
        {
            if (param.star_3)
            {
                ConfigQuestMissionRecord quest_3 = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(param.config_ms.Quest_3);
                AddCash(quest_3.Reward, null);
                missionData.star_3 = true;
            }

        }
        else
        {
            isNewMission = false;
        }
        dataModel.UpdateDataDictionary<MissionData>(DataPath.DIC_MISSION, param.config_ms.id.Tokey(),missionData);

        if(isNewMission)
        {
            int id = param.config_ms.id;
            if(id<10)// max mission 
            {
                id++;
                MissionData missionData_new = new MissionData();
                dataModel.UpdateDataDictionary<MissionData>(DataPath.DIC_MISSION, id.Tokey(), missionData_new);
            }
        
        }
    }
}
