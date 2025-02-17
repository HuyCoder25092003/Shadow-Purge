using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionDetailQuest : MonoBehaviour
{
    public TMP_Text description;
    public TMP_Text reward;
    public GameObject star;
   
    // Start is called before the first frame update
   public void Setup(int id_quest, bool isDone)
    {
        star.SetActive(isDone);
        ConfigQuestMissionRecord cf = ConfigManager.instance.configQuestMission.GetRecordByKeySearch(id_quest);
        description.text = cf.Desciption;
        reward.text = $"{cf.Reward}";
        reward.transform.parent.gameObject.SetActive(!isDone);
    }
}
