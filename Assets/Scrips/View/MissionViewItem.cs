using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionViewItem : EnhancedScrollerCellView
{
    public Image iconMission;
    public TMP_Text mission_index;
    public TMP_Text mission_name;
    public GameObject lock_object;
    public GameObject star_parent_object;
    public GameObject[] starts;
    public GameObject cur_select;
    private MissionViewListData mission_data_view;
   public void SetDataCell(MissionViewListData data)
    {
        this.mission_data_view = data;

        mission_index.text = $"{data.cf_mission.id}";
        iconMission.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName($"Mission_Image_{data.cf_mission.id}");
        mission_name.text = data.cf_mission.Name;
        if(data.mission_data == null)
        {
            lock_object.SetActive(true);
            star_parent_object.SetActive(false);
            cur_select.SetActive(false);
        }
        else
        {
            star_parent_object.SetActive(true);
            lock_object.SetActive(false);
            starts[0].SetActive(data.mission_data.star_1);
            starts[1].SetActive(data.mission_data.star_2);
            starts[2].SetActive(data.mission_data.star_3);
            cur_select.SetActive(data.mission_data.star_1 == false&& data.mission_data.star_2 == false&& data.mission_data.star_3 == false);
        }
    }
        
    public void OnSelected()
    {
        MissionDetailDialogParam param = new MissionDetailDialogParam { data = this.mission_data_view };
        DialogManager.instance.ShowDialog(DialogIndex.MissionDetailDialog,param);
    }
}
