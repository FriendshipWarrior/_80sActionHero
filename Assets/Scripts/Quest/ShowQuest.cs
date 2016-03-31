using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This actually shows the Quest at the top of the screen, you can change it
//via the Rect
public class ShowQuest : MonoBehaviour {

	//GUI
	private Vector3 scale;
	public float originalWidth = 1280.0f;
	public float originalHeight = 720.0f;

	//GUI Current Quest
	private GameObject _PlayerGUI;

	//Old GUI
	private EasyQuestManager _Manager;
	private bool ShowCompleted = false;
	public bool NewGUI = false;
	public Rect Pos;
	public GUISkin skin;

	//New GUI
	//Description of the quest Text object
	public Text QuestDescrip;
	//Quest Objetive Obj
	public Text QuestObj;

	void OnEnable () {
		//Get our references
		_PlayerGUI = GameObject.Find ("PlayerGUI");
		_Manager = GameObject.FindGameObjectWithTag("EQManager").GetComponent<EasyQuestManager>();
		//Once we complete a quest, call the event.
		EasyQuestManager.OnQuestFinish += CompleteQuest;
	}

	void OnDisable(){
		EasyQuestManager.OnQuestFinish -= CompleteQuest;
	}

	void CompleteQuest(){
		//Used to show 'COMPLETED QUEST' in the courner for a few seconds
		StartCoroutine (QC ());
	}

	IEnumerator QC(){
		//Used to show 'COMPLETED QUEST' in the courner for a few seconds
		ShowCompleted = true;
		yield return new WaitForSeconds(2);
		ShowCompleted = false;
	}

	void Update(){
		//If we're not using legacy UI, show this.
		if (NewGUI) {
			if(_Manager.CurrentQuest != null){
				QuestDescrip.text = _Manager.CurrentQuest.name;

				if (_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].qType == QuestType.GoTo) {
					QuestObj.text = "GoTo: " + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].World;
				}
				if (_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].qType == QuestType.Collect) {
					QuestObj.text = "Collect " + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].Name + " at " +
						_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].World + ". " + 
							_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].Amount + "/" + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].AmountNeeded;
				}
			}
		}
	}

	void OnGUI(){
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1.0f;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0,0,0), Quaternion.identity, scale);
		GUI.skin = skin;

		GUILayout.BeginArea (Pos);
		if (NewGUI == false) {
			//If we actually have a quest, check what type it is and show information
			if (_Manager.CurrentQuest != null) {
				if (_Manager.CurrentQuest.FinishedQuest == false) {
					GUILayout.BeginVertical ();
					GUILayout.Label ("Current Quest: " + _Manager.CurrentQuest.QuestName);
					GUILayout.Box (_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].sDescript, GUILayout.Height (50));
					if (_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].qType == QuestType.GoTo) {
						GUILayout.Box ("GoTo: " + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].World);
					}
					if (_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].qType == QuestType.Collect) {
						GUILayout.Label ("Location: " + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].World);
						GUILayout.Label ("Collect: " + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].Name);
						GUILayout.Label (_Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].Amount + "/" + _Manager.CurrentQuest.Objs [_Manager.CurrentQuest.CObjective].AmountNeeded);
					}
					GUILayout.EndVertical ();
				}
			} else {
				//Else we just have no quest
				GUILayout.Label ("Current Quest: ...");
			}

			if (ShowCompleted) {
				//Show 'COMPLETED QUEST' in corner
				GUILayout.Label ("COMPLETED QUEST!");
			}
		}
		GUILayout.EndArea ();

		GUI.matrix = svMat; // restore matrix
	}
}
