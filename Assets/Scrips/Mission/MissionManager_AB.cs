using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager_AB : MissionManager
{
    // Start is called before the first frame update
    public override void InitMission(ConfigMissionRecord cf)
    {
        base.InitMission(cf);

        Debug.LogError(" create new misison AB");
    }
}
