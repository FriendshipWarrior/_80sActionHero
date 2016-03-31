using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class QuestGiverUI : MonoBehaviour {
	
	private GameObject player; //The player object
	private bool WindowOpen = false;
	//Quest
	private EasyQuestManager _Manager; //A reference to the EasyQuestManager
	private EasyQuest Quest; //The referenece to the quest we give
	private TextMesh text; //The current text we have stored from the Quest
	private bool ComPQuest; //If you've completed previous quest
	private int OverallID;
	public int ProgressID; //The Progress ID of the quest
	public int QuestID; //The ID of the quest needed
	public float Distance = 2; //Distance you need to be to get quest
	public bool Progression = false; //If this quest needs the previous one done
	
	//GUI
	private ModalPanel modalPanel;
	private UnityAction AcceptAction;
	private UnityAction DenyAction;

	void Awake(){
		modalPanel = ModalPanel.Instance ();
		//Set actions
		AcceptAction = new UnityAction (OnYes);
		DenyAction = new UnityAction (OnNo);
	}

	void Start(){
		//Grabbing our references
		player = GameObject.FindGameObjectWithTag ("Player");
		_Manager = GameObject.FindGameObjectWithTag("EQManager").GetComponent<EasyQuestManager>();
		text = GetComponentInChildren<TextMesh>();

		//Quest Giver getting the quest needed
		for(int i = 0; i < _Manager.EQList.Count; i++){
			if(_Manager.EQList[i].QuestID == QuestID && _Manager.EQList[i].ProgressionID == ProgressID){
				Quest = _Manager.EQList[i];
				OverallID = Quest.OverallID;
				return;
			}
		}
	}
	
	void Update(){
		if (text) {
			if (Quest.FinishedQuest) {
				text.text = "...";
			} else if(Quest.CurrentQuest){
				text.text = "?";
			}else{
				text.text = "!";
			}
		}

		//For detecting controllers
		if (Input.GetButtonUp ("Submit") && Vector3.Distance(player.transform.position, transform.position) <= Distance &&
		    WindowOpen == false) {
			OnMouseDown();
		}
	}
	
	void OnMouseDown() {
		ComPQuest = true;
		//Checks if previous quest have been finsihed if this quest follows a progressive line.
		ComPQuest = _Manager.CheckQuestLine(QuestID, ProgressID);
		
		//Deciding if we should show the GUI or not
		if(Vector3.Distance(player.transform.position, transform.position) <= Distance){
			if(_Manager.CurrentQuest == null){
				if(ComPQuest == true){
					if (Quest.CurrentQuest == false && Quest.FinishedQuest == false) {
						WindowOpen = true;
						modalPanel.Choice(Quest.Description, AcceptAction, DenyAction);
					}
				}
			}
		}
	}

	public void OnYes(){
		_Manager.AddQuest(OverallID);
		StartCoroutine (DelayBeforeClose());
	}

	public void OnNo(){
		modalPanel.ClosePanel ();
		StartCoroutine (DelayBeforeClose());
	}

	IEnumerator DelayBeforeClose(){
		//Waits a few seconds, so that when the window it closed it's not picking up the same button press
		//that you used to close it.
		yield return new WaitForSeconds (0.5f);
		WindowOpen = false;
	}

}
