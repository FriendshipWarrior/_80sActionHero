using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {

    public static GameObject control;
    [HideInInspector]
    //All quest that are children of this object
    public List<Quest> QList = new List<Quest>();
    [HideInInspector]
    //Questlog
    public List<Quest> QuestList = new List<Quest>();
    [HideInInspector]
    //Current main quest
    public Quest CurrentQuest;
    //Order by ID
    public bool QuestProgression = true;
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this.gameObject;
        }else if(control != this.gameObject)
        {
            Destroy(gameObject);
        }
        //Gets all quest under the manager
        QList.AddRange(GetComponentsInChildren<Quest>());

        //Load
        QuestSaver.qSaver.LoadPlayerData(QList.Count);
        //Set the loaded stuff
        for (int i = 0;i< QList.Count; i++)
        {
            SetQuest(QList[i].OverallID);
        }
        //then save whatever we loaded
        for(int i = 0; i < QList.Count; i++)
        {
            UpdateQuest(QList[i].OverallID);
        }
    }   
    /// <summary>
    /// 
    /// </summary>
    public void LevelLoad()
    {
        //calls the event
        OnLevelLoaded();
    }	
    /// <summary>
    /// when we need to add a quest to the list
    /// </summary>
    /// <param name="OverallID"></param>
    public void AddQuest(int OverallID)
    {
        if(CurrentQuest == null)
        {
            //Sets a current quest if we dont have one.
            for(int i = 0; i < QList.Count; i++)
            {
                if(QList[i].OverallID == OverallID)
                {
                    CurrentQuest = QList[i];
                    CurrentQuest.CurrentQuest = true;
                    if(OnQuestGet != null)
                    {
                        OnQuestGet();
                    }
                    UpdateQuest(OverallID);
                    return;
                }
            }
        }
        else
        {
            //Else just add the quest to the list log
            for(int i = 0; i < QList.Count; i++)
            {
                if(QList[i].OverallID == OverallID)
                {
                    QuestList.Add(QList[i]);
                    return;
                }
            }
        }
    }
    /// <summary>
    /// Run this when quest finishes
    /// </summary>
    /// <param name="OverallID"></param>
    public void QuestFinish(int OverallID)
    {
        //Checking what index the quest is at
        int temp = -1;
        for(int q = 0; q < QuestSaver.qSaver.QuestOverallID.Count; q++)
        {
            if(QuestSaver.qSaver.QuestOverallID[q] == OverallID)
            {
                temp = q;
                break;
            }
        }
        if(temp == -1)
        {
            return;
        }
        //Finish
        //Once a quest has finished, add it to the finished list and remove it from the current list
        QuestSaver.qSaver.FinishedQuest[temp] = true;
        QuestSaver.qSaver.CurrentQuest[temp] = false;
        CurrentQuest = null;
        if(OnQuestFinish != null)
        {
            OnQuestFinish();
        }
        CheckList();

        //save
        for(int i = 0; i < QList.Count; i++)
        {
            UpdateQuest(QList[i].OverallID);
        }
    }
    /// <summary>
    /// Sets quest data to what was loaded
    /// </summary>
    /// <param name="OverallID"></param>
	public void SetQuest(int OverallID)
    {
        //Used to get data ready to be changed for the quest.
        int Q = -1;
        for (int i = 0; i < QList.Count; i++)
        {
            if (QList[i].OverallID == OverallID)
            {
                Q = i;
                break;
            }
        }

        if (Q != -1)
        {
            //Checking what index the quest is at, since we can't keep track of it normally in a List :(
            int temp = -1;
            for (int q = 0; q < QuestSaver.qSaver.QuestOverallID.Count; q++)
            {
                if (QuestSaver.qSaver.QuestOverallID[q] == OverallID)
                {
                    temp = q;
                    break;
                }
            }
            if (temp == -1)
            {
                return;
            }

            //Setting variables here to what we received from the load.
            QList[Q].FinishedQuest = QuestSaver.qSaver.FinishedQuest[temp];
            QList[Q].CurrentQuest = QuestSaver.qSaver.CurrentQuest[temp];
            QList[Q].AvaliableQuest = QuestSaver.qSaver.AvaliableQuest[temp];
            QList[Q].ReturnQuest = QuestSaver.qSaver.ReturnQuest[temp];
            QList[Q].CObjective = QuestSaver.qSaver.CObjective[temp];
            QList[Q].AmountOfO = QuestSaver.qSaver.AmountOfO[temp];

            CheckList();
        }
    }
    /// <summary>
    /// updates a quest info for saving purposes
    /// </summary>
    /// <param name="OverallID"></param>
    public void UpdateQuest(int OverallID)
    {
        //Used to save all current data of a quest
        int Q = -1;
        for (int i = 0; i < QList.Count; i++)
        {
            if (QList[i].OverallID == OverallID)
            {
                Q = i;
                break;
            }
        }
        //Checking what index the quest is at, since we can't keep track of it normally in a List :(
        int temp = -1;
        for (int q = 0; q < QuestSaver.qSaver.QuestOverallID.Count; q++)
        {
            if (QuestSaver.qSaver.QuestOverallID[q] == OverallID)
            {
                temp = q;
                break;
            }
        }
        if (temp == -1)
        {
            return;
        }

        //Set the info.
        if (Q != -1)
        {
            QuestSaver.qSaver.QuestOverallID[temp] = QList[Q].OverallID;
            QuestSaver.qSaver.FinishedQuest[temp] = QList[Q].FinishedQuest;
            QuestSaver.qSaver.CurrentQuest[temp] = QList[Q].CurrentQuest;
            QuestSaver.qSaver.AvaliableQuest[temp] = QList[Q].AvaliableQuest;
            QuestSaver.qSaver.ReturnQuest[temp] = QList[Q].ReturnQuest;
            QuestSaver.qSaver.CObjective[temp] = QList[Q].CObjective;
            QuestSaver.qSaver.AmountOfO[temp] = QList[Q].AmountOfO;
            QuestSaver.qSaver.SavePlayerData();
        }
    }
    /// <summary>
    /// Check the last quest in the questline before the one you reference with QuestID, to make sure it's complete
    /// </summary>
    /// <param name="QuestID"></param>
    /// <param name="ProgressID"></param>
    /// <returns></returns>
    public bool CheckQuestLine(int QuestID, int ProgressID)
    {
        //Makes sure we are not the only quest in the line
        if ((QuestID - 1) == 0)
        {
            return true;
        }
        for (int i = 0; i < QList.Count; i++)
        {
            //Make sure it's in the same questline.
            if (QList[i].ProgressionID == ProgressID)
            {
                //Check if last quest before this is finished
                if (QList[i].QuestID == (QuestID - 1) && QList[i].FinishedQuest == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
    /// <summary>
    /// Checks list to set our next current quest
    /// </summary>
    public void CheckList()
    {
        //Check that we don't have a current quest.
        if (CurrentQuest == null)
        {
            if (QuestList.Count == 0)
            {
                //First, check if a quest was already the current and we forgot to set it
                //when we loaded our save.
                for (int i = 0; i < QList.Count; i++)
                {
                    if (QList[i].CurrentQuest == true)
                    {
                        AddQuest(QList[i].OverallID);
                        return;
                    }
                }
            }
            else {
                //Else, set the current one to the first one in the list.
                int temp = -1;
                for (int q = 0; q < QuestSaver.qSaver.QuestOverallID.Count; q++)
                {
                    if (QuestSaver.qSaver.QuestOverallID[q] == QuestList[0].OverallID)
                    {
                        temp = q;
                        break;
                    }
                }
                if (temp == -1)
                {
                    return;
                }

                CurrentQuest = QuestList[0];
                CurrentQuest.CurrentQuest = true;
                QuestSaver.qSaver.CurrentQuest[temp] = true;
            }
        }
    }

    //Events
    public delegate void OnQGet(); //Once you get a quest
    public static event OnQGet OnQuestGet;

    public delegate void OnQFinish(); //Once you finish a quest
    public static event OnQFinish OnQuestFinish;

    public delegate void OnQProgressed(); //Once you progress a quest
    public static event OnQProgressed OnQuestProgress;

    public delegate void OnLLoaded(); //Once a new level loads
    public static event OnQProgressed OnLevelLoaded;

}

