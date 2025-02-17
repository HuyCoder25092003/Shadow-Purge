using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionViewListData
{
    public ConfigMissionRecord cf_mission;
    public MissionData mission_data;
}
public class MissionViewList : MonoBehaviour, IEnhancedScrollerDelegate
{
    /// <summary>
    /// Internal representation of our data. Note that the scroller will never see
    /// this, so it separates the data from the layout using MVC principles.
    /// </summary>
    private List<MissionViewListData> data_list;

    /// <summary>
    /// This is our scroller we will be a delegate for
    /// </summary>
    public EnhancedScroller scroller;

    /// <summary>
    /// This will be the prefab of each cell in our scroller. Note that you can use more
    /// than one kind of cell, but this example just has the one type.
    /// </summary>
    public EnhancedScrollerCellView cellViewPrefab;
    private MissionViewListData cur_data;
    int index_cur = -1;

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        // first, we get a cell from the scroller by passing a prefab.
        // if the scroller finds one it can recycle it will do so, otherwise
        // it will create a new cell.
        MissionViewItem cellView = scroller.GetCellView(cellViewPrefab) as MissionViewItem;

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
    public void Setup()
    {
        data_list = new List<MissionViewListData>();
        Dictionary<string, MissionData> dic = DataAPIController.instance.GetMissionData();
        int i = 0;
        foreach (ConfigMissionRecord cf in ConfigManager.instance.configMission.GetAllRecord())
        {
            MissionViewListData data = new MissionViewListData();
            data.cf_mission = cf;
            MissionData mission_data = null;
            dic.TryGetValue(cf.id.Tokey(), out mission_data);
            data.mission_data = mission_data;
            if (mission_data != null)
            {
                if (data.mission_data.star_1 == false && data.mission_data.star_2 == false && data.mission_data.star_3 == false)
                {
                    cur_data = data;
                    index_cur = i;
                }
            }
            data_list.Add(data);
            i++;
        }

        scroller.ReloadData();
        Invoke("DelayJump", 0.1f);

    }
    private void DelayJump()
    {
        scroller.JumpToDataIndex(index_cur, 0.4f, 0);
    }
    void Start()
    {
        scroller.Delegate = this;
    }


}
