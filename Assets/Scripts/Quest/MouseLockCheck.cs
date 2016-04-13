using UnityEngine;
using System.Collections;

public class MouseLockCheck : MonoBehaviour {

	//Variables that scripts check before they decide if they should handle locking mouse
	//If quest window open
	[HideInInspector]
	public bool QuestWindowOpen = false;

	public void Start(){
		//Just in case, set it to false once the game starts.
		QuestWindowOpen = false;
	}

	public void Update(){
		if (QuestWindowOpen || Input.GetKeyUp(KeyCode.Escape)) {
			//If the Quest Window is open, or if the hero presses escape, unlock the cursor.
			Screen.lockCursor = false;
		} else if(Input.GetMouseButtonUp(0)){
			//Else, if the mouse is clicked relock the cursor.
			Screen.lockCursor = true;
		}
	}
}
