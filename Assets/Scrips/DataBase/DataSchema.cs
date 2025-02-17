using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData 
{
    [SerializeField]
    public UserInfo info;
    [SerializeField]
    public Dictionary<string, MissionData> dic_misson = new Dictionary<string, MissionData>();
    [SerializeField]
    public Inventory inventory;
}
[Serializable]
public class UserInfo
{
    public string username;
    public int level;
    public int exp;
    [SerializeField]
    public List<int> guns_equip = new List<int>();
}
[Serializable]
public class Inventory
{
    public int cash;
    [SerializeField]
    public Dictionary<string, WeaponData> dic_weapon = new Dictionary<string, WeaponData>();
}

[Serializable]
public class WeaponData
{
    public int id;
    public int level;
}
[Serializable]
public class MissionData
{
    public int id;
    public bool star_1;
    public bool star_2;
    public bool star_3;
}

public class DataPath
{
    public const string CASH = "inventory/cash";
    public const string DIC_WEAPON = "inventory/dic_weapon";
    public const string INFO = "info";
    public const string GUNS_EQUIP = "info/guns_equip";
    public const string DIC_MISSION = "dic_misson";
}