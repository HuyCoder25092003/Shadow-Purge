using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionView : BaseView
{
    public MissionViewList missionViewList;
    public override void Setup(ViewParam data)
    {
        missionViewList.Setup();
    }
    public void OnBack()
    {
        ViewManager.instance.SwitchView(ViewIndex.HomeView);
    }
}
