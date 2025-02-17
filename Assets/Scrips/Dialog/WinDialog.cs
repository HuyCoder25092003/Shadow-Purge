using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinDialog : BaseDialog
{
    public Image iconMission;
    public TMP_Text mission_name;
    public MissionDetailQuest[] quests;
    // Start is called before the first frame update
    public override void Setup(DialogParam data)
    {
        base.Setup(data);
        WinDialogParam d_param = data as WinDialogParam;

        iconMission.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName($"Mission_Image_{d_param.config_ms.id}");
        mission_name.text = d_param.config_ms.Name;
        quests[0].Setup(d_param.config_ms.Quest_1, d_param.star_1);
        quests[1].Setup(d_param.config_ms.Quest_2, d_param.star_2);
        quests[2].Setup(d_param.config_ms.Quest_3, d_param.star_3);
    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
        LoadSceneManager.instance.LoadSceneByIndex(1, () =>
        {
            ViewManager.instance.SwitchView(ViewIndex.WeaponView);
        });
    }
   
}
