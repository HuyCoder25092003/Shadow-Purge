using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionManager : BySingleton<MissionManager>
{
    public ConfigMissionRecord cf_mision;
    public int index_wave=-1;
    public List<int> wave_ids;
    public int number_enemy_dead_wave;
    public ConfigWaveRecord cf_wave;
    public UnityEvent<int,int> OnWaveChange;
    private CharacterControl characterControl;
    private WeaponControl weaponControl;

    public virtual void InitMission(ConfigMissionRecord cf)
    {
        cf_mision = cf;
        //create player 
        GameObject logic = Instantiate(Resources.Load("Player/Logic", typeof(GameObject))) as GameObject;
        logic.transform.position = SceneConfig.instance.player_pos.position;
        characterControl = logic.GetComponentInChildren<CharacterControl>();
        weaponControl = logic.GetComponentInChildren<WeaponControl>();
    }
    public virtual void OnObjectCollect(object data)
    {

    }
    public virtual void EnemyOnDead(EnemyControl enemyControl, Bulletdata bulletdata)
    {
        Quest_4_data quest_4_Data = new Quest_4_data();
        quest_4_Data.weaponType = bulletdata.cf_wp.WP_Type;
        quest_4_Data.index_wave = index_wave;
        quest_4_Data.max_enemy = cf_wave.Total;

        QuestLogData quest_4 = new QuestLogData();
        quest_4.questType = MissionQuestType.Quest_4;
        quest_4.data = quest_4_Data;
        QuestManager.instance.LogQuest(quest_4);

    }
    public void WinMission()
    {

        // quest
        QuestLogData quest_3 = new QuestLogData();
        quest_3.questType = MissionQuestType.Quest_3;
        quest_3.data = characterControl.HP;
        QuestManager.instance.LogQuest(quest_3);

        QuestLogData quest_5 = new QuestLogData();
        quest_5.questType = MissionQuestType.Quest_5;
        quest_5.data = weaponControl.num_reload;
        QuestManager.instance.LogQuest(quest_5);

        WinDialogParam param = new WinDialogParam();
        param.config_ms = cf_mision;
        param.star_1 = QuestManager.instance.CheckQuest(1, out param.isNew_1);
        param.star_2 = QuestManager.instance.CheckQuest(2, out param.isNew_2);
        param.star_3 = QuestManager.instance.CheckQuest(3, out param.isNew_3);

        DialogManager.instance.ShowDialog(DialogIndex.WinDialog, param);
        DataAPIController.instance.UpdateMissionWin(param);
    }
    private void OnDisable()
    {
        OnWaveChange.RemoveAllListeners();
    }
}
