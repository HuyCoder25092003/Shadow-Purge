using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigDefault", menuName = "BY/ConfigDefault", order = 1)]
public class ConfigDefault : ScriptableObject
{
    public int max_damage=50;
    public int max_range=100;
    public int cash = 100;
    public List<int> weapon_ids;

}
