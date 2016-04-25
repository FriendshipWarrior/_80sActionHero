using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
	
	public static GameObject control;
	[HideInInspector]
	//All Quest that are children of this object
	public List<Quest> QList = new List<Quest>();
	[HideInInspector]
	//Our questlog (AKA all active quest)
	public List<Quest> QuestList = new List<Quest> ();
	[HideInInspector]
	//Our current main quest to complete
	public Quest CurrentQuest;
	//If the quest need to go in order by there ID
	public bool QuestProgesssion = true;

    //QuestDatabase database;
    //public List<Quests> quests = new List<Quests>();

    void Awake()
    {
		if(control == null)
        {
			DontDestroyOnLoad(gameObject);
			control = this.gameObject;
		}
        else if(control != this.gameObject)
        {
			Destroy(gameObject);
		}

        //database = GetComponent<QuestDatabase>();

        //Gets all quest under the manager
        QList.AddRange (GetComponentsInChildren<Quest>());
        for(int i = 0; i < QList.Count; i++)
        {
            Debug.Log(QList[i].QuestName);
        }
		//Load
		QuestSaver.qSaver.LoadPlayerData (QList.Count);
		//Set the stuff we loaded
		for (int i = 0; i < QList.Count; i++)
        {
			SetQuest (QList[i].OverallID);
		}
		//..Then save whatever we loaded, just in case.
		for (int i = 0; i < QList.Count; i++)
        {
			UpdateQuest (QList[i].OverallID);
		}
	}

	public void LevelLoad()
    {
		//Calls the event
		OnLevelLoaded ();
	}

	//When we need to add a quest to the Quest list, we do it here.
	public void AddQuest(int OverallID)
    {
		if (CurrentQuest == null)
        {
			//Sets a current quest if we don't have one.
			for (int i = 0; i < QList.Count; i++)
            {
				if (QList [i].OverallID == OverallID)
                {
					CurrentQuest = QList [i];
					CurrentQuest.CurrentQuest = true;
					if (OnQuestGet != null)
                    {
							OnQuestGet ();
					}
					UpdateQuest (OverallID);
					return;
				}
			}
		}
        else
        {
			//Else just add the quest to the list/quest log.
			for (int i = 0; i < QList.Count; i++)
            {
				if (QList [i].OverallID == OverallID)
                {
					QuestList.Add (QList [i]);
					return;
				}
			}
		}
	}

	//Once a quest finishes, it runs this.
	public void QuestFinish(int OverallID)
    {
        //Checking what index the quest is at, since we can't keep track of it normally in a List :(
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
        //Once a quest is finished, add it to the finished quest list and remove it from the current quest list.
		QuestSaver.qSaver.FinishedQuest [temp] = true;
		QuestSaver.qSaver.CurrentQuest [temp] = false;
        CurrentQuest = null;
		if (OnQuestFinish != null)
        {
			OnQuestFinish ();
		}
		CheckList ();
		//Save.
		for (int i = 0; i < QList.Count; i++)
        {
			UpdateQuest (QList[i].OverallID);
		}
	}

	//Sets quest data to what we've loaded
	public void SetQuest(int OverallID)
    {
		//Used to get data ready to be changed for the quest.
		int EQ = -1;
		for (int i = 0; i < QList.Count; i++)
        {
			if(QList[i].OverallID == OverallID)
            {
				EQ = i;
				break;
			}
		}

		if (EQ != -1)
        {
			//Checking what index the quest is at, since we can't keep track of it normally in a List :(
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

			//Setting variables here to what we received from the load.
			QList[EQ].FinishedQuest = QuestSaver.qSaver.FinishedQuest [temp];
			QList[EQ].CurrentQuest = QuestSaver.qSaver.CurrentQuest [temp];
			QList[EQ].AvaliableQuest = QuestSaver.qSaver.AvaliableQuest [temp];
			QList[EQ].ReturnQuest = QuestSaver.qSaver.ReturnQuest [temp];
			QList[EQ].CObjective = QuestSaver.qSaver.CObjective [temp];
			QList[EQ].AmountOfO = QuestSaver.qSaver.AmountOfO [temp];

			CheckList();
		}
	}

	//updates a quest info for saving purposes.
	public void UpdateQuest(int OverallID)
    {
		//Used to save all current data of a quest
		int EQ = -1;
		for (int i = 0; i < QList.Count; i++)
        {
			if(QList[i].OverallID == OverallID)
            {
				EQ = i;
				break;
			}
		}
		//Checking what index the quest is at, since we can't keep track of it normally in a List :(
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

		//Set the info.
		if (EQ != -1)
        {
			QuestSaver.qSaver.QuestOverallID[temp] = QList[EQ].OverallID;
			QuestSaver.qSaver.FinishedQuest[temp] = QList[EQ].FinishedQuest;
			QuestSaver.qSaver.CurrentQuest[temp] = QList[EQ].CurrentQuest;
			QuestSaver.qSaver.AvaliableQuest[temp] = QList[EQ].AvaliableQuest;
			QuestSaver.qSaver.ReturnQuest[temp] = QList[EQ].ReturnQuest;
			QuestSaver.qSaver.CObjective[temp] = QList[EQ].CObjective;
			QuestSaver.qSaver.AmountOfO[temp] = QList[EQ].AmountOfO;
			QuestSaver.qSaver.SavePlayerData ();
		}
	}

	//Check the last quest in the questline before the one you reference with QuestID, to make sure it's complete
	public bool CheckQuestLine(int QuestID, int ProgressID)
    {
		//Makes sure we are not the only quest in the line
		if((QuestID-1) == 0)
        {
			return true;
		}
		for (int i = 0; i < QList.Count; i++)
        {
			//Make sure it's in the same questline.
			if(QList[i].ProgressionID == ProgressID)
            {
				//Check if last quest before this is finished
				if(QList[i].QuestID == (QuestID-1) && QList[i].FinishedQuest == true)
                {
					return true;
				}
			}
		}
		return false;
	}

	//Checks list to set our next current quest
	public void CheckList()
    {
		//Check that we don't have a current quest.
		if (CurrentQuest == null)
        {
			if(QuestList.Count == 0)
            {
				//First, check if a quest was already the current and we forgot to set it
				//when we loaded our save.
				for (int i = 0; i < QList.Count; i++)
                {
					if (QList [i].CurrentQuest == true)
                    {
						AddQuest(QList[i].OverallID);
						return;
					}
				}
			}
            else
            {
				//Else, set the current one to the first one in the list.
				int temp = -1;
				for (int q = 0; q < QuestSaver.qSaver.QuestOverallID.Count; q++)
                {
					if (QuestSaver.qSaver.QuestOverallID [q] == QuestList [0].OverallID)
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
