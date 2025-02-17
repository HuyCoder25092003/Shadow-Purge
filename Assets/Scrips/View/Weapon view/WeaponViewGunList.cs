using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponListData
{
    public ConfigWeaponRecord cf;
    public WeaponView weaponView;
}
public class WeaponViewGunList : MonoBehaviour, IEnhancedScrollerDelegate
{
    public WeaponView weaponView;
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<WeaponListData> data_list;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. Note that you can use more
    /// than one kind of cell, but this example just has the one type.
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        WeaponViewGunListItem cellView = scroller.GetCellView(cellViewPrefab) as WeaponViewGunListItem;

        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = $"Cell {dataIndex}";

        // in this example, we just pass the data to our cell's view which will update its UI
        cellView.SetDataCell(data_list[dataIndex]);

        // return the cell to the scroller
        return cellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 600;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return data_list.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        scroller.Delegate = this;
    }
    public void OnAll()
    {
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordBuyWeapon())
        {
            data_list.Add(new WeaponListData { cf = cf ,weaponView=weaponView});
        }
       
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(1);
        Invoke("DelayJump", 0.1f);
    }
    private void DelayJump()
    {
        scroller.RefreshActiveCellViews();

    }
    public void OnAssault()
    {
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordBuyWeaponType(WeaponType.Assault))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.1f);
    }
    public void OnHandGun()
    {
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordBuyWeaponType(WeaponType.Handgun))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.1f);
    }
    public void OnShotgun()
    {
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordBuyWeaponType(WeaponType.Shotgun))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.1f);

    }
    public void OnMachine()
    {
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordBuyWeaponType(WeaponType.Machine))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }
        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.1f);

    }
    public void OnMiniGun()
    {
        data_list = new List<WeaponListData>();
        foreach (ConfigWeaponRecord cf in ConfigManager.instance.configWeapon.GetRecordBuyWeaponType(WeaponType.Special))
        {
            data_list.Add(new WeaponListData { cf = cf, weaponView = weaponView });
        }

        // tell the scroller to reload now that we have the data
        scroller.ReloadData();
        scroller.JumpToDataIndex(0);
        Invoke("DelayJump", 0.1f);

    }
}
