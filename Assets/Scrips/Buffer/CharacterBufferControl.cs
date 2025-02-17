using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBufferControl : MonoBehaviour
{
    public Transform anchor_wp;
    public Animator animator;
    private Dictionary<string, GameObject> dic_guns = new Dictionary<string, GameObject>();
    private GameObject cur_gun;
    void Start()
    {
        SetGunEquipInfo();
    }
    public  void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataPath.INFO, OnWeaponChange);
    }
    public  void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataPath.CASH, OnWeaponChange);
    }

    private void OnWeaponChange(object dataChange)
    {
        SetGunEquipInfo();
    }
    private void SetGunEquipInfo()
    {
        UserInfo userInfo = DataAPIController.instance.GetUserInfo();
        List<int> gun_ids = userInfo.guns_equip;
        WeaponData wp_data = DataAPIController.instance.GetWeaponDataById(gun_ids[0]);
        ConfigWeaponRecord cf_gun = ConfigManager.instance.configWeapon.GetRecordByKeySearch(wp_data.id, wp_data.level);
        if(cur_gun!=null)
        {
            cur_gun.SetActive(false);
        }
        if (dic_guns.ContainsKey(cf_gun.Prefab))
        {
            cur_gun = dic_guns[cf_gun.Prefab];
        }
        else
        {
            cur_gun = Instantiate(Resources.Load("Weapons UI/" + cf_gun.Prefab, typeof(GameObject))) as GameObject;
            cur_gun.transform.SetParent(anchor_wp, false);
            dic_guns[cf_gun.Prefab] = cur_gun;
        }
        cur_gun.SetActive(true);
        animator.runtimeAnimatorController = cur_gun.GetComponent<WeaponUI>().overrideController;
        animator.Play("Draw", 0, 0);
    }
}
