using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public enum MissionQuestType
{
    /// <summary>
    /// Win mission
    /// </summary>
    Quest_1 = 1,
    /// <summary>
    /// Kill All enemies
    /// </summary>
    Quest_2 = 2,
    /// <summary>
    ///Win mission with HP > x % 
    /// </summary>
    Quest_3 = 3,
    /// <summary>
    ///Kill all wave enemy with shotgun
    /// </summary>
    Quest_4 = 4,

    /// <summary>
    ///Win mission with x magazines
    /// </summary>
    Quest_5 = 5,
    /// <summary>
    ///Win mission with only hand gun
    /// </summary>
    Quest_6 = 6,
    /// <summary>
    ///Kill x enemys with a magazine
    /// </summary>
    Quest_7 = 7,
    /// <summary>
    ///one shot one kill , x enemies
    /// </summary>
    Quest_8 = 8,

    /// <summary>
    /// Keep Survivor's HP x%
    /// </summary>
    Quest_9 = 9,

}
public class QuestLogData
{
    public MissionQuestType questType;
    public object data;
}

public class EnemyQuestLogData : QuestLogData
{
    public ConfigEnemyRecord cf_enemy;
}
public class QuestManager : BySingleton<QuestManager>
{
    public event Action<QuestLogData> OnLogQuestEvent;
    private List<QuestItemControl> quest_items = new List<QuestItemControl>();
    public bool isNew_1;
    public bool isNew_2;
    public bool isNew_3;
    public void InitQuest(ConfigMissionRecord cf_mission)
    {
        MissionData missionData = DataAPIController.instance.GetMissionDataID(cf_mission.id);
        if (missionData.star_1 == false)
        {
            ConfigQuestMissionRecord quest_1 = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(cf_mission.Quest_1);

            GameObject quest_object_1 = Instantiate(Resources.Load($"Quest/{quest_1.Type}", typeof(GameObject))) as GameObject;
            quest_object_1.transform.SetParent(transform);
            QuestItemControl questItemControl_1 = quest_object_1.GetComponent<QuestItemControl>();
            questItemControl_1.Setup(quest_1);
            questItemControl_1.index = 1;
            quest_items.Add(questItemControl_1);
            isNew_1 = true;
            this.OnLogQuestEvent += questItemControl_1.LogQuest;
        }
        else
        {
            isNew_1 = false;
        }
        if (missionData.star_2 == false)
        {
            ConfigQuestMissionRecord quest_2 = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(cf_mission.Quest_2);

            GameObject quest_object_2 = Instantiate(Resources.Load($"Quest/{quest_2.Type}", typeof(GameObject))) as GameObject;
            quest_object_2.transform.SetParent(transform);
            QuestItemControl questItemControl_2 = quest_object_2.GetComponent<QuestItemControl>();
            questItemControl_2.Setup(quest_2);
            questItemControl_2.index = 2;
            quest_items.Add(questItemControl_2);
            isNew_2 = true;
            this.OnLogQuestEvent += questItemControl_2.LogQuest;
        }
        else
        {
            isNew_2 = false;
        }
        if (missionData.star_3 == false)
        {
            ConfigQuestMissionRecord quest_3 = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(cf_mission.Quest_3);

            GameObject quest_object_3 = Instantiate(Resources.Load($"Quest/{quest_3.Type}", typeof(GameObject))) as GameObject;
            quest_object_3.transform.SetParent(transform);
            QuestItemControl questItemControl_3 = quest_object_3.GetComponent<QuestItemControl>();
            questItemControl_3.Setup(quest_3);
            questItemControl_3.index = 3;
            quest_items.Add(questItemControl_3);
            isNew_3 = true;
            this.OnLogQuestEvent += questItemControl_3.LogQuest;
        }
        else
        {
            isNew_3 = false;
        }
    }

    public void LogQuest(QuestLogData questLogData)
    {
        OnLogQuestEvent?.Invoke(questLogData);
    }
    public bool CheckQuest(int index, out bool isNew)
    {
        isNew = false;
        if (index == 1)
            isNew = isNew_1;
        if (index == 2)
            isNew = isNew_2;
        if (index == 3)
            isNew = isNew_3;
        foreach (QuestItemControl item in quest_items)
        {
            if (item.index == index)
            {
                return item.CheckQuest();
            }
        }

        return quest_items[index].CheckQuest();
    }

}
