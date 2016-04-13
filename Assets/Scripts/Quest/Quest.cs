
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//All quest types
public enum QuestType{
	GoTo, Collect, Defeat, MoveObj
}

[Serializable]
public class Quest : MonoBehaviour {
	//All objectives for this quest
	[SerializeField]
	public List<ObjectiveT> Objs = new List<ObjectiveT>();
	//Name of the quest
	[SerializeField]
	public string QuestName;
	//The ID in general (Used for saving/loading quest)
	[SerializeField]
	public int OverallID;
	//The ID of the quest
	[SerializeField]
	public int QuestID;
	//The progression line ID
	[SerializeField]
	public int ProgressionID;
	//The quest description
	[SerializeField]
	public string Description;
	[SerializeField]
	//The complete quest dialogue
	public string CompleteQ;
	[SerializeField]
	//The speech audioclip
	public AudioClip Speech;
	[SerializeField]
	//The main hero's tag
	public string heroT;

    public int expToGive;

	//Mission info
	private GameObject hero;
    private HeroStats heroStats;
	public QuestManager manager;
	public bool CurrentQuest = false; //If the quest is in the hero's log
	public bool FinishedQuest = false; //If the quest is finished
	public bool AvaliableQuest = false; //If the quest is avaliable
	public bool ReturnQuest = false; //If the quest has to be returned
	public bool RewardPlayer = false; //If the quest rewards the hero
	public int CObjective = 0; // the current Objective in the mission
	public int AmountOfO = 1; //Total of Objectives

	void OnEnable()
    {
		//Listen for when a new level is loaded.
		QuestManager.OnLevelLoaded += OnLevelLoad;
	}

	void OnDisable()
    {
		QuestManager.OnLevelLoaded -= OnLevelLoad;
	}

	void Awake()
    {
		//Get our references.
		manager = GameObject.FindGameObjectWithTag("QManager").GetComponent<QuestManager>();
		hero = GameObject.FindGameObjectWithTag (heroT);
        heroStats = FindObjectOfType<HeroStats>();
    }

	void Start()
    {
        //Make sure that if we're the currentquest, actually tell the manager that.
        if (CurrentQuest)
        {
			manager.CheckList();
		}
	}

	void Update()
    {
        //Checking if quest is complete
        if (CurrentQuest)
        {
			if(Objs[CObjective].Complete == false)
            {
				//Collect quest
				if(Objs[CObjective].qType == QuestType.Collect)
                {
					if(Objs[CObjective].Amount == Objs[CObjective].AmountNeeded)
                    {
						if(CObjective == AmountOfO - 1)
                        {
                            heroStats.AddExperience(expToGive);
                            Objs[CObjective].Complete = true;
							FinishedQuest = true;
							CurrentQuest = false;
							manager.QuestFinish(OverallID);
                        }
                        else
                        {
							CObjective += 1;
						}
					}
				}
				//Defeat quest
				if(Objs[CObjective].qType == QuestType.Defeat)
                {
					if(Objs[CObjective].Amount == Objs[CObjective].AmountNeeded)
                    {
						if(CObjective == AmountOfO - 1)
                        {
                            heroStats.AddExperience(expToGive);
                            Objs[CObjective].Complete = true;
							FinishedQuest = true;
							CurrentQuest = false;
							manager.QuestFinish(OverallID);
						}
                        else
                        {
							CObjective += 1;
						}
					}
				}
				//GoTo quest
				if(Objs[CObjective].qType == QuestType.GoTo)
                {
                    if (Vector3.Distance(Objs[CObjective].Destination, hero.transform.position) <= Objs[CObjective].MaxDist)
                    {
						if(Application.loadedLevelName == Objs[CObjective].World)
                        {
							if(CObjective == AmountOfO - 1)
                            {
                                heroStats.AddExperience(expToGive);
                                Objs[CObjective].Complete = true; //Finish Quest
								FinishedQuest = true;
								CurrentQuest = false;
								manager.QuestFinish(OverallID);
                            }
                            else
                            {
								CObjective += 1;
							}
						}
					}
				}
				//Move Object Quest
				if(Objs[CObjective].qType == QuestType.MoveObj)
                {
					if(Vector3.Distance(Objs[CObjective].ObjToMove.transform.position, Objs[CObjective].Destination) <= Objs[CObjective].MaxDist)
                    {
						if(Application.loadedLevelName == Objs[CObjective].World)
                        {
							if(CObjective == AmountOfO-1)
                            {
								Objs[CObjective].Complete = true;
								FinishedQuest = true;
								CurrentQuest = false;
								manager.QuestFinish(OverallID);
							}
                            else
                            {
								CObjective += 1;
							}
						}
					}
				}
			}
		}
	}

	void OnLevelLoad()
    {
		//Sets it's parent to the QuestManager
		transform.parent = manager.gameObject.transform;
	}

	void OnLevelWasLoaded(int level)
    {
		//Once the level is loaded, call Awake()
		Awake ();
	}
}

[System.Serializable]
public class ObjectiveT
{

	public QuestType qType; //The type of quest
	public bool Complete = false; //If the quest is completed
	public string World; //The level the person has to be in
	public string sDescript; //Short description

	//Coolect/Defeat Quest
	public string Name; //Name of object
	public int AmountNeeded = 1; //Amount needed
	public int Amount = 0; //Our current amount

	//GoTo Quest
	public Vector3 Destination; //Destination coordinates
	public float MaxDist = 5; //Distance around the point

	//Move Quest
	public GameObject ObjToMove; //The object to move
}
