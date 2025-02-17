using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ConfigQuestMissionRecord
{
    //id	name	cost	value	bonus	

    public int id;
    [SerializeField]
    private MissionQuestType type;
    public MissionQuestType Type
    {
        get
        {
            return type;
        }
    }
    [SerializeField]
    private string desciption;
    public string Desciption
    {
        get
        {
            return desciption;
        }
    }

 
  
    [SerializeField]
    private int reward;
    public int Reward
    {
        get
        {
            return reward;
        }
    }


}
public class ConfigQuestMission : BYDataTable<ConfigQuestMissionRecord>
{
    public override ConfigCompare<ConfigQuestMissionRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigQuestMissionRecord>("id");
        return configCompare;
    }
    public List<MissionQuestType> GetAllMissionQuestType()
    {
        List<MissionQuestType> ls = new List<MissionQuestType>();
        foreach (ConfigQuestMissionRecord e in records)
        {
            if (!ls.Contains(e.Type))
                ls.Add(e.Type);
        }
        return ls;

    }
}
