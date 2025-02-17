using UnityEngine;
using System.Collections;

public class Quest_5 : QuestItemControl {
    public int number_magazine = 10;
     public override void Setup(ConfigQuestMissionRecord configQuest)
     {
         base.Setup(configQuest);
     }
     public override void LogQuest(QuestLogData logData)
     {
        if (logData.questType == MissionQuestType.Quest_5)
        {
            int num = (int)logData.data;
            if (num <= number_magazine)
                isDone = true;
        }
    }
     public override bool CheckQuest()
     {
         return base.CheckQuest();
     }
}
