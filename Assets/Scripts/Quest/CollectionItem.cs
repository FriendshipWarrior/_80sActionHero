using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionItem : MonoBehaviour {

	public bool DestroyObjAfter;
    public int itemID;

    private List<Quest> Q = new List<Quest>();
    private QuestManager Manager;
    private Inventory inv;

    void Start()
    {
		//Get our references
		Manager = (QuestManager)FindObjectOfType (typeof(QuestManager));
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

	//Updates any quest that needs the item 
	void OnMouseDown ()
    {
		//Get all the quest we have
		Q = Manager.QList;
		//Iterate through them
		for(int i = 0; i < Q.Count; i++)
        {
			//Iterate through the objectives
			for(int s = 0; s < Q[i].AmountOfO; s++)
				//Check if the quest is the right one
				if(Q[i].CurrentQuest && Q[i].Objs[s].qType == QuestType.Collect || Q[i].CurrentQuest && Q[i].Objs[s].qType == QuestType.Defeat)
                {
					//Make sure the objective isn't completed	
					if(Q[i].Objs[s].Amount < Q[i].Objs[s].AmountNeeded)
                    {
						//Check to see if it's the right object
						if(gameObject.name == Q[i].Objs[s].Name)
                        {
							//Make sure we're in the right world for the quest
							if(Application.loadedLevelName == Q[i].Objs[s].World || Q[i].Objs[s].World == "")
                            {
								//Add to the amount
								Q[i].Objs[s].Amount += 1;
                                inv.AddItem(itemID);
								if(DestroyObjAfter)
                                {
									Destroy(gameObject);
								}
							}
						}
					}
				}
		}
	}
}
