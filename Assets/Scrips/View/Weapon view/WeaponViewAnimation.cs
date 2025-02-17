using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponViewAnimation : BaseViewAnimation
{
    public RectTransform top;
    public RectTransform bottom;
    public RectTransform left;
    public override void OnHideView(Action callback)
    {
        top.DOAnchorPosY(250, 0.5f).OnComplete(() => {
         
            callback();
        });
        bottom.DOAnchorPosY(-600, 0.5f);
        left.DOAnchorPosX(1200, 0.25f);
    }
    public override void OnShowView(Action callback)
    {
        top.DOAnchorPosY(0, 0.5f).OnComplete(()=>{
            callback();
        });
        bottom.DOAnchorPosY(0, 0.5f);
        left.DOAnchorPosX(0, 0.25f);
    }
}
