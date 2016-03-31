using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionItem : MonoBehaviour {

	public bool DestroyObjAfter;
    public int itemID;

    private List<EasyQuest> EQ = new List<EasyQuest>();
    private EasyQuestManager Manager;
    private Inventory inv;

    void Start(){
		//Get our references
		Manager = (EasyQuestManager)FindObjectOfType (typeof(EasyQuestManager));
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

	//Updates any quest that needs the item 
	void OnMouseDown (){
		//Get all the quest we have
		EQ = Manager.EQList;
		//Iterate through them
		for(int i = 0; i < EQ.Count; i++){
			//Iterate through there objectives
			for(int s = 0; s < EQ[i].AmountOfO; s++)
				//Check if the quest is the right one
				if(EQ[i].CurrentQuest && EQ[i].Objs[s].qType == QuestType.Collect || EQ[i].CurrentQuest && EQ[i].Objs[s].qType == QuestType.Defeat){
					//Make sure the objective isn't completed	
					if(EQ[i].Objs[s].Amount < EQ[i].Objs[s].AmountNeeded){
						//Check to see if it's the right object
						if(gameObject.name == EQ[i].Objs[s].Name){
							//Make sure we're in the right world for the quest
							if(Application.loadedLevelName == EQ[i].Objs[s].World || EQ[i].Objs[s].World == ""){
								//Add to the amount
								EQ[i].Objs[s].Amount += 1;
                                inv.AddItem(itemID);
								if(DestroyObjAfter){
									Destroy(gameObject);
								}
							}
						}
					}
				}
		}
	}
}
