using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ConfigShopRecord
{
    //id	name	cost	value	bonus	

    public int id;
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
    private string cost;
    public string Cost
    {
        get
        {
            return cost;
        }
    }
  

    [SerializeField]
    private int value;
    public int Value
    {
        get
        {
            return value;
        }
    }

    [SerializeField]
    private int bonus;
    public int Bonus
    {
        get
        {
            return bonus;
        }
    }

}
public class ConfigShop : BYDataTable<ConfigShopRecord>
{
    public override ConfigCompare<ConfigShopRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigShopRecord>("id");
        return configCompare;
    }
}
