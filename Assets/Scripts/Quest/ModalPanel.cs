using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//Made so that the hero doesn't need references himself, and so that we can just set them before the game starts.
//Useful for networking and such.
public class ModalPanel : MonoBehaviour {
	//Gets the references needed for the quest dialogue.
	public GameObject ModalPanelObj;
	public Text QuestDialogue; //Dialogue of the quest.
	public Image Portrait; //Portrait of NPC; not needed.
	public Button AcceptButton;
	public Button DenyButton;

	private static ModalPanel modalPanel;

	public static ModalPanel Instance(){
		//Check if we already have a ModalPanel instance. If not, make this the current modalPanel instance.
		if (!modalPanel) {
			modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
			if(!modalPanel){
				Debug.LogError("No quest panel found. Create one with 'ModalPanel.cs' attached");
			}
		}

		return modalPanel;
	}

	//Handles Accept/Deny buttons and whatever you want them to do. In this case, it's for the quest dialogue.
	public void Choice(string QuestDialogue, UnityAction acceptEvent, UnityAction denyEvent){
		//Turns on the quest window
		ModalPanelObj.SetActive (true);
		//Locks camera
		GetComponent<MouseLockCheck> ().QuestWindowOpen = true;

		//Remove our last quest(s) that listened to the buttons, and add the current one.
		AcceptButton.onClick.RemoveAllListeners();
		DenyButton.onClick.RemoveAllListeners();

		//Allows us to listen to the onCLick event without setting it in the editor.
		AcceptButton.onClick.AddListener (acceptEvent);
		AcceptButton.onClick.AddListener (ClosePanel);
		DenyButton.onClick.AddListener (denyEvent);
		DenyButton.onClick.AddListener (ClosePanel);

		this.QuestDialogue.text = QuestDialogue;
		Portrait.gameObject.SetActive(false);
		AcceptButton.gameObject.SetActive (true);
		DenyButton.gameObject.SetActive (true);
	}

	public void ClosePanel(){
		//Closes panel (disables it)
		ModalPanelObj.SetActive (false);
		//Unlocks Camera
		GetComponent<MouseLockCheck> ().QuestWindowOpen = false;
	}
}
