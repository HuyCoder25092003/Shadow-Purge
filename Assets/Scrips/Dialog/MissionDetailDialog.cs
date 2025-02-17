using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionDetailDialog : BaseDialog
{
    public Image iconMission;
    public TMP_Text mission_name;
    MissionViewListData mission_view_data;
    public MissionDetailQuest[] quests;
    // Start is called before the first frame update
    public override void Setup(DialogParam data)
    {
        base.Setup(data);
        MissionDetailDialogParam d_param = data as MissionDetailDialogParam;
        this.mission_view_data = d_param.data;
        iconMission.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName("Mission_Image_" +  this.mission_view_data.cf_mission.id.ToString());
        mission_name.text = this.mission_view_data.cf_mission.Name;
        quests[0].Setup(this.mission_view_data.cf_mission.Quest_1, this.mission_view_data.mission_data.star_1);
        quests[1].Setup(this.mission_view_data.cf_mission.Quest_2, this.mission_view_data.mission_data.star_2);
        quests[2].Setup(this.mission_view_data.cf_mission.Quest_3, this.mission_view_data.mission_data.star_3);
    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
    public void OnFight()
    {
        Debug.LogError(" mission id: " + mission_view_data.cf_mission.id);
        DialogManager.instance.HideDialog(dialogIndex);
        ConfigMissionRecord configMissionRecord = mission_view_data.cf_mission;

        LoadSceneManager.instance.LoadSceneByName(configMissionRecord.Scene_name, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.EmptyView);
            GameManager.instance.InitMission(configMissionRecord);
        });
    }
}
