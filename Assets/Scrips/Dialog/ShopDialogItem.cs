using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopDialogItem : MonoBehaviour
{
    public int id;

    public TMP_Text name_lb;
    public TMP_Text bonus_lb;
    public TMP_Text cost_lb;
    public TMP_Text value_lb;
    private ConfigShopRecord cf;
    void Start()
    {
        cf = ConfigManager.instance.configShop.GetRecordByKeySearch(id);
        name_lb.text = cf.Name;
        bonus_lb.text = cf.Bonus > 0 ? $"{cf.Bonus}%" : "";
        cost_lb.text = cf.Cost;
        value_lb.text = cf.Value.ToString();
    }
    public void OnBuy()
    {
        DataAPIController.instance.OnBuyShop(id);
    }
}
