using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class QuestDatabase : MonoBehaviour {
    private List<Quests> database = new List<Quests>();
    private JsonData questData;

    void Start()
    {
        questData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Quests.json"));
        ConstructQuestDatabase();

        
        for(int i = 0; i < database.Count; i++)
        {
            Debug.Log(FetchItemByID(1));
        }
        
    }

    public Quests FetchItemByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].OverallID == id)
                return database[i];
        }
        return null;
    }

    void ConstructQuestDatabase()
    {
        for (int i = 0; i < questData.Count; i++)
        {
            database.Add(new Quests(
                questData[i]["questName"].ToString(),
                (int)questData[i]["overallID"],
                (int)questData[i]["progressID"],
                (int)questData[i]["questID"],
                questData[i]["questDescription"].ToString(),
                questData[i]["completeQ"].ToString(),
                questData[i]["heroTag"].ToString(),
                (int)questData[i]["expToGive"],
                questData[i]["questType"].ToString(),
                questData[i]["world"].ToString(),
                questData[i]["shortSummary"].ToString(),
                questData[i]["itemName"].ToString(),
                questData[i]["enemyName"].ToString(),
                (int)questData[i]["amountNeeded"],
                (int)questData[i]["position"]["x"],
                (int)questData[i]["position"]["y"],
                (int)questData[i]["position"]["z"],
                (int)questData[i]["compensation"]));
        }
    }
}

public class Quests
{
    public string QuestName { get; set; }
    public int OverallID { get; set; }
    public int ProgressID { get; set; }
    public int QuestID { get; set; }
    public string QuestDescription { get; set; }
    public string CompleteQ { get; set; }
    public string HeroTag { get; set; }
    public int ExpToGive { get; set; }
    public string QuestType { get; set; }
    public string World { get; set; }
    public string ShortSummary { get; set; }
    public string ItemName { get; set; }
    public string EnemyName { get; set; }
    public int AmountNeeded { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public int Compensation { get; set; }


    public Quests(string questName, int  overallID, int progressID, int questID, 
        string questDescription, string completeQ, string heroTag, int expToGive, string questType, 
            string world, string shortSummary, string itemName, string enemyName, int amountNeeded, int x, int y, int z, int compensation)
    {
        this.QuestName = questName;
        this.OverallID = overallID;
        this.ProgressID = progressID;
        this.QuestID = questID;
        this.QuestDescription = QuestDescription;
        this.CompleteQ = completeQ;
        this.HeroTag = heroTag;
        this.ExpToGive = expToGive;
        this.QuestType = QuestType;
        this.World = world;
        this.ShortSummary = shortSummary;
        this.ItemName = itemName;
        this.EnemyName = enemyName;
        this.AmountNeeded = amountNeeded;
        this.X = x;
        this.Y = y;
        this.Z = z;
        this.Compensation = compensation;
    }

    public Quests()
    {
        this.OverallID = -1;
    }
}
