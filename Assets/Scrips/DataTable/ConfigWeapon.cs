using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum WeaponType
{
    Assault = 1,
    Handgun = 2,
    Shotgun = 3,
    Machine = 4,
    Special = 5
}
[Serializable]
public class ConfigWeaponRecord
{
    //id	weaponType	name	prefab	rof	range	damge	accuracy_min	accuracy_max	reload

    public int id;
    [SerializeField]
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
    }
    [SerializeField]
    private WeaponType weaponType;
    public WeaponType WP_Type
    {
        get
        {
            return weaponType;
        }
    }
    [SerializeField]
    private string name;
    public string Name
    {
        get
        {
            return name;
        }
    }
    [SerializeField]
    private string prefab;
    public string Prefab
    {
        get
        {
            return prefab;
        }
    }
    [SerializeField]
    private float rof;
    public float ROF
    {
        get
        {
            return rof;
        }
    }
    [SerializeField]
    private int range;
    public int Range
    {
        get
        {
            return range;
        }
    }
    [SerializeField]
    private int damge;
    public int Damge
    {
        get
        {
            return damge;
        }
    }
    [SerializeField]
    private int accuracy_min;
    public int Accuracy_min
    {
        get
        {
            return accuracy_min;
        }
    }
    [SerializeField]
    private int accuracy_max;
    public int Accuracy_max
    {
        get
        {
            return accuracy_max;
        }
    }
    [SerializeField]
    private float reload;
    public float Reload
    {
        get
        {
            return reload;
        }
    }
    [SerializeField]
    private int price;
    public int Price
    {
        get
        {
            return price;
        }
    }
}
public class ConfigWeapon : BYDataTable<ConfigWeaponRecord>
{
    public override ConfigCompare<ConfigWeaponRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigWeaponRecord>("id", "level");
        return configCompare;
    }
    public List<ConfigWeaponRecord> GetRecordBuyWeapon(int level = 1)
    {
        return records.Where(x => x.Level == level).ToList();
    }
    public List<ConfigWeaponRecord> GetRecordBuyWeaponType(WeaponType wp_type, int level = 1)
    {
        return records.Where(x => x.WP_Type == wp_type && x.Level == level).ToList();
    }
    public int GetMaxLevel(int id)
    {
        return records.Where(x => x.id == id).Count();
    }
}
