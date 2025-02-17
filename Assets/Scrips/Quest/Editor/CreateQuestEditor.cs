using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;


public class CreateQuestEditor : MonoBehaviour
{
    [MenuItem("Assets/Create Quest Behaviours Script")]
    public static void CreateQuestBehaviourScript(MenuCommand cmd)
    {


        ConfigQuestMission configQuest = Resources.Load("DataTable/ConfigQuestMission", typeof(ScriptableObject)) as ConfigQuestMission;
        foreach (MissionQuestType e in configQuest.GetAllMissionQuestType())
        {
            string name = e.ToString();
            string classParent = "QuestItemControl";

            string copyPath = "Assets/Scrips/Quest/" + name + ".cs";


            if (File.Exists(copyPath) == false)
            { // do not overwrite
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine("using UnityEngine;");
                    outfile.WriteLine("using System.Collections;");
                    outfile.WriteLine("");
                    outfile.WriteLine("public class " + name + " : " + classParent + " {");
                    outfile.WriteLine("     public override void Setup(ConfigQuestMissionRecord configQuest)");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("         base.Setup(configQuest);");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     public override void LogQuest(QuestLogData logData)");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("         base.LogQuest(logData);");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     public override bool CheckQuest()");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("         return base.CheckQuest();");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     void Start()");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     void Update()");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("     }");

                    outfile.WriteLine("}");
                }//File written
            }

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


    }

    [MenuItem("Assets/Create Quest Behaviours Prefab")]
    public static void CreateQuestBehaviourPrefab(MenuCommand cmd)
    {






        ConfigQuestMission configQuest = Resources.Load("DataTable/ConfigQuestMission", typeof(ScriptableObject)) as ConfigQuestMission;
        foreach (MissionQuestType e in configQuest.GetAllMissionQuestType())
        {
            string name = e.ToString();
            Debug.LogError(name);
            string localPath = "Assets/Resources/Quest/" + name + ".prefab";
            if (File.Exists(localPath) == false)
            {
                GameObject obj = new GameObject();
                Type com = Type.GetType(name);
                obj.AddComponent(com);
                // Create the new Prefab.

                PrefabUtility.SaveAsPrefabAsset(obj, localPath);
            }


        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


    }
}
