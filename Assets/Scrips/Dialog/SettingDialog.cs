using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingDialog : BaseDialog
{
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
}
