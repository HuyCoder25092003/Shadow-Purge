using UnityEngine;
using System.Collections;
public class Quest_4_data
{
    public int index_wave;
    public int max_enemy;
    public WeaponType weaponType;
}
public class Quest_4 : QuestItemControl {
    public WeaponType weaponType;
    private int num_enemy;
    public Quest_4_data cur_quest_3_Data = new Quest_4_data();
    public Quest_4_data new_quest_data;

    public override void Setup(ConfigQuestMissionRecord configQuest)
    {
        base.Setup(configQuest);
    }
    public override void LogQuest(QuestLogData logData)
    {
        if (logData.questType == MissionQuestType.Quest_1)
        {
            new_quest_data = (Quest_4_data)logData.data;
            if (new_quest_data.index_wave != cur_quest_3_Data.index_wave)
            {
                cur_quest_3_Data = new_quest_data;
                num_enemy = 0;
            }
            else
            {
                if (new_quest_data.weaponType == weaponType)
                {
                    num_enemy++;
                    if (num_enemy >= cur_quest_3_Data.max_enemy)
                    {
                        isDone = true;
                    }
                }
                else
                {

                }
            }



        }
    }
    public override bool CheckQuest()
    {
        return isDone;
    }
}
