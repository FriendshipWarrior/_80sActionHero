using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

//Handles actually saving Quest and all there info
public class QuestSaver : MonoBehaviour
{

    public static QuestSaver qSaver;
    //Quest
    [HideInInspector]
    public List<int> QuestOverallID = new List<int>();
    [HideInInspector]
    public List<bool> CurrentQuest = new List<bool>();
    [HideInInspector]
    public List<bool> FinishedQuest = new List<bool>();
    [HideInInspector]
    public List<bool> AvaliableQuest = new List<bool>();
    [HideInInspector]
    public List<bool> ReturnQuest = new List<bool>();
    [HideInInspector]
    public List<int> CObjective = new List<int>();
    [HideInInspector]
    public List<int> AmountOfO = new List<int>();

    public string GameName = "QS";
    public string GameAbbreviation = "QS";

    void Awake()
    {
        if (qSaver == null)
        {
            DontDestroyOnLoad(gameObject);
            qSaver = this;
        }
        else if (qSaver != this)
        {
            Destroy(gameObject);
        }
        //Prints the path where the game is saved, if you wanna check that.
        //Debug.Log (Application.persistentDataPath.ToString());
    }

    void OnDisable()
    {
        SavePlayerData();
    }

    public void SetAmounts(int Amount)
    {
        //Sets up the List to be ready to edit with no errors popping up
        if (QuestSaver.qSaver.FinishedQuest.Count == 0)
        {
            for (int i = 0; i < Amount; i++)
            {
                //Add one, so that OverallID starts at 1.
                QuestSaver.qSaver.QuestOverallID.Add(i + 1);
                QuestSaver.qSaver.FinishedQuest.Add(false);
                QuestSaver.qSaver.CurrentQuest.Add(false);
                QuestSaver.qSaver.AvaliableQuest.Add(true);
                QuestSaver.qSaver.ReturnQuest.Add(false);
                QuestSaver.qSaver.CObjective.Add(0);
                QuestSaver.qSaver.AmountOfO.Add(1);
            }
        }
    }

    public void SavePlayerData()
    {
        //We're going to check our platform, as webplayer needs a different way to save/load.
        if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
        {

            int[] QuestOverallIDArray = QuestOverallID.ToArray();
            bool[] CurrentQuestArray = CurrentQuest.ToArray();
            bool[] FinishedQuestArray = FinishedQuest.ToArray();
            bool[] AvaliableQuestArray = AvaliableQuest.ToArray();
            bool[] ReturnQuestArray = ReturnQuest.ToArray();
            int[] CObjectiveQuestArray = CObjective.ToArray();
            int[] AmountOfOArray = AmountOfO.ToArray();

            PlayerPrefsX.SetIntArray("OverallID", QuestOverallIDArray);
            PlayerPrefsX.SetBoolArray("CurrentQuest", CurrentQuestArray);
            PlayerPrefsX.SetBoolArray("FinishedQuest", FinishedQuestArray);
            PlayerPrefsX.SetBoolArray("AvaliableQuest", AvaliableQuestArray);
            PlayerPrefsX.SetBoolArray("ReturnQuest", ReturnQuestArray);
            PlayerPrefsX.SetIntArray("CObjective", CObjectiveQuestArray);
            PlayerPrefsX.SetIntArray("AmountOfO", AmountOfOArray);
        }
        else {
            //else we're on a platform that can save to device
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/" + GameName + "." + GameAbbreviation);

            PlayerData data = new PlayerData();
            data.OverallQuestID = QuestOverallID;
            data.CurrentQuest = CurrentQuest;
            data.FinishedQuest = FinishedQuest;
            data.AvaliableQuest = AvaliableQuest;
            data.ReturnQuest = ReturnQuest;
            data.CObjective = CObjective;
            data.AmountOfO = AmountOfO;

            bf.Serialize(file, data);
            file.Close();
        }
    }

    public void LoadPlayerData(int QuestAmount)
    {
        //If on webplayer, use PlayerPrefsX to load data
        if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer)
        {
            int[] OverallIDArray;
            bool[] CurrentQuestArray;
            bool[] FinishedQuestArray;
            bool[] AvaliableQuestArray;
            bool[] ReturnQuestArray;
            int[] CObjectiveQuestArray;
            int[] AmountOfOArray;

            //Set array data
            OverallIDArray = PlayerPrefsX.GetIntArray("OverallID");
            CurrentQuestArray = PlayerPrefsX.GetBoolArray("CurrentQuest");
            FinishedQuestArray = PlayerPrefsX.GetBoolArray("FinishedQuest");
            AvaliableQuestArray = PlayerPrefsX.GetBoolArray("AvaliableQuest");
            ReturnQuestArray = PlayerPrefsX.GetBoolArray("ReturnQuest");
            CObjectiveQuestArray = PlayerPrefsX.GetIntArray("CObjective");
            AmountOfOArray = PlayerPrefsX.GetIntArray("AmountOfO");

            //Convert array to list
            QuestOverallID = OverallIDArray.ToList();
            CurrentQuest = CurrentQuestArray.ToList();
            FinishedQuest = FinishedQuestArray.ToList();
            AvaliableQuest = AvaliableQuestArray.ToList();
            ReturnQuest = ReturnQuestArray.ToList();
            CObjective = CObjectiveQuestArray.ToList();
            AmountOfO = AmountOfOArray.ToList();

            if (QuestOverallID.Count == 0)
            {
                //If the data is empty, create default data.
                SetAmounts(QuestAmount);
            }

        }
        else {
            //Else just load from the system.
            if (File.Exists(Application.persistentDataPath + "/" + GameName + "." + GameAbbreviation))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + GameName + "." + GameAbbreviation, FileMode.Open);
                PlayerData data = (PlayerData)bf.Deserialize(file);
                file.Close();

                QuestOverallID = data.OverallQuestID;
                CurrentQuest = data.CurrentQuest;
                FinishedQuest = data.FinishedQuest;
                AvaliableQuest = data.AvaliableQuest;
                ReturnQuest = data.ReturnQuest;
                CObjective = data.CObjective;
                AmountOfO = data.AmountOfO;

                //If the data is empty, create default data.
                if (QuestOverallID.Count == 0)
                {
                    SetAmounts(QuestAmount);
                }
            }
            else {
                //If the data is empty, create default data.
                SetAmounts(QuestAmount);
            }
        }
    }
}

[Serializable]
class PlayerData
{
    public List<int> OverallQuestID = new List<int>();
    public List<bool> CurrentQuest = new List<bool>();
    public List<bool> FinishedQuest = new List<bool>();
    public List<bool> AvaliableQuest = new List<bool>();
    public List<bool> ReturnQuest = new List<bool>();
    public List<int> CObjective = new List<int>();
    public List<int> AmountOfO = new List<int>();
}

