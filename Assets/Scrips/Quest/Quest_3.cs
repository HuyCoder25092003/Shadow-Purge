using UnityEngine;
using System.Collections;

public class Quest_3 : QuestItemControl
{

    public int per_hp = 60;
    public override void Setup(ConfigQuestMissionRecord configQuest)
    {
        base.Setup(configQuest);
    }
    public override void LogQuest(QuestLogData logData)
    {
        if (logData.questType == MissionQuestType.Quest_3)
        {
            int hp = (int)logData.data;
            if (hp >= per_hp)
                isDone = true;
        }
    }
}
