using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemControl : MonoBehaviour
{
    public bool isDone = false;
    /// <summary>
    /// index quest 1,2,3
    /// </summary>
    public int index = 0;
   // public QuestData questData;
    public ConfigQuestMissionRecord configQuest;
    public virtual void Setup(ConfigQuestMissionRecord configQuest)
    {
        this.configQuest = configQuest;
    }
    public virtual void LogQuest(QuestLogData logData)
    {

    }
    public virtual bool CheckQuest()
    {
        return false;
    }
}
