using UnityEngine;
using System.Collections;

public class Quest_2 : QuestItemControl {
     public override void Setup(ConfigQuestMissionRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
        if (logData.questType == MissionQuestType.Quest_1)
        {
            isDone = true;
        }
    }
     public override bool CheckQuest()
     {
         return isDone;
     }
}
