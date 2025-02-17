using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionType
{
    KillAll = 1,
    AB = 2,
    Protection = 3
}
[Serializable]
public class ConfigMissionRecord
{
    //id	name	description	waves	reward

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
    private string description;
    public string Description
    {
        get
        {
            return description;
        }
    }
    [SerializeField]
    private MissionType mission_type;
    public MissionType Mission_type
    {
        get
        {
            return mission_type;
        }
    }

    [SerializeField]
    private string scene_name;
    public string Scene_name
    {
        get
        {
            return scene_name;
        }
    }
    [SerializeField]

    private string waves;
    public List<int> Waves
    {
        get
        {
            List<int> ls = new List<int>();
            string[] s_array = waves.Split(':');
            foreach (string e in s_array)
            {
                ls.Add(int.Parse(e));
            }
            return ls;
        }
    }

    [SerializeField]
    private int quest_1;
    public int Quest_1
    {
        get
        {
            return quest_1;
        }
    }
    [SerializeField]
    private int quest_2;
    public int Quest_2
    {
        get
        {
            return quest_2;
        }
    }
    [SerializeField]
    private int quest_3;
    public int Quest_3
    {
        get
        {
            return quest_3;
        }
    }

}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override ConfigCompare<ConfigMissionRecord> DefineConfigCompare()
    {
        configCompare = new ConfigCompare<ConfigMissionRecord>("id");
        return configCompare;
    }
}
