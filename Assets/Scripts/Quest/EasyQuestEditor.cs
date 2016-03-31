using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EasyQuest))]
public class EasyQuestEditor : Editor {
	
	public GameObject Player;
	public EasyQuest EQ;
	
	public override void OnInspectorGUI(){
		EQ = target as EasyQuest;
		EditorStyles.textField.wordWrap = true;
		
		//Quest Name
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Quest Name: ", GUILayout.Width(85));
		EQ.QuestName = EditorGUILayout.TextField (EQ.QuestName, GUILayout.Width(225));
		GUILayout.EndHorizontal ();
		//Quest ID
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Overall ID: ", GUILayout.Width(85));
		EQ.OverallID = int.Parse(GUILayout.TextField (EQ.OverallID.ToString(), GUILayout.Width(125)));
		GUILayout.EndHorizontal ();
		//Progression ID
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Progress ID: ", GUILayout.Width(85));
		EQ.ProgressionID = int.Parse(GUILayout.TextField (EQ.ProgressionID.ToString(), GUILayout.Width(125)));
		GUILayout.EndHorizontal ();
		//Quest ID
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Quest ID: ", GUILayout.Width(85));
		EQ.QuestID = int.Parse(GUILayout.TextField (EQ.QuestID.ToString(), GUILayout.Width(125)));
		GUILayout.EndHorizontal ();
		//Quest Description
		GUILayout.BeginVertical ();
		GUILayout.Label("Quest Description: ", GUILayout.Width(125));
		EQ.Description = EditorGUILayout.TextField (EQ.Description, GUILayout.Height(80));
		GUILayout.EndVertical ();
		//Complete Quest
		GUILayout.BeginVertical ();
		GUILayout.Label("Complete Quest Message: ", GUILayout.Width(175));
		EQ.CompleteQ = EditorGUILayout.TextField (EQ.CompleteQ, GUILayout.Height(20));
		GUILayout.EndVertical ();
		//Player(s)
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Player Tag: ", GUILayout.Width(85));
		EQ.PlayerT = EditorGUILayout.TextField (EQ.PlayerT, GUILayout.Width(125));
		GUILayout.EndHorizontal ();
		//Add/Remove Objectives
		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Add Objective")) {
			EQ.AmountOfO += 1;
		}
		if (GUILayout.Button ("Remove Objective")) {
			EQ.AmountOfO -= 1;
		}
		GUILayout.EndHorizontal();
		
		
		if(EQ.Objs.Count < EQ.AmountOfO){
			EQ.Objs.Add(new ObjectiveT());
		}
		for(int i = 0; i < EQ.AmountOfO; i++){
			//Quest type
			GUILayout.BeginHorizontal ();
			GUILayout.Label("Quest Type: ", GUILayout.Width(85));
			EQ.Objs[i].qType = (QuestType)EditorGUILayout.EnumPopup(EQ.Objs[i].qType);
			GUILayout.EndHorizontal ();
			//World Name
			GUILayout.BeginHorizontal();
			GUILayout.Label("World: ");
			EQ.Objs[i].World = EditorGUILayout.TextField(EQ.Objs[i].World);
			GUILayout.EndHorizontal();
			//Short Description
			GUILayout.BeginHorizontal();
			GUILayout.Label("Short Summary: ", GUILayout.Width(100));
			EQ.Objs[i].sDescript = EditorGUILayout.TextField(EQ.Objs[i].sDescript);
			GUILayout.EndHorizontal();
			GUILayout.Space(2);
			
			
			ShowQuestOpt (i);
			GUILayout.Space(14);
		}
	}
	
	void ShowQuestOpt(int iE){
		if(EQ.Objs[iE].qType == QuestType.GoTo){
			if(EQ.Objs.Count < EQ.AmountOfO){
				EQ.Objs.Add(new ObjectiveT());
			}else{
				//Position to go to
				GUILayout.BeginHorizontal();
				GUILayout.Label("Position: ", GUILayout.Width(100));
				EQ.Objs[iE].Destination = EditorGUILayout.Vector3Field("", EQ.Objs[iE].Destination);
				GUILayout.EndHorizontal();
				GUILayout.Space(2);
				//COmpensation
				GUILayout.BeginHorizontal();
				GUILayout.Label("Compensation: ", GUILayout.Width(100));
				EQ.Objs[iE].MaxDist = EditorGUILayout.FloatField(EQ.Objs[iE].MaxDist);
				GUILayout.EndHorizontal();
			}
		}
		if (EQ.Objs[iE].qType == QuestType.Collect || EQ.Objs[iE].qType == QuestType.Defeat) {
			//Item Name
			GUILayout.BeginHorizontal();
			if(EQ.Objs[iE].qType == QuestType.Collect){
				GUILayout.Label("Item Name: ", GUILayout.Width(100));
			}
			if(EQ.Objs[iE].qType == QuestType.Defeat){
				GUILayout.Label("Enemy Name: ", GUILayout.Width(100));
			}
			EQ.Objs[iE].Name = EditorGUILayout.TextField(EQ.Objs[iE].Name);
			GUILayout.EndHorizontal();
			//Amount Needed
			GUILayout.BeginHorizontal();
			GUILayout.Label("Amount Needed: ", GUILayout.Width(100));
			EQ.Objs[iE].AmountNeeded = int.Parse(EditorGUILayout.TextField(EQ.Objs[EQ.CObjective].AmountNeeded.ToString()));
			GUILayout.EndHorizontal();
		}
		if (EQ.Objs[iE].qType == QuestType.MoveObj) {
			//Position to go to
			GUILayout.BeginHorizontal();
			GUILayout.Label("Destination: ", GUILayout.Width(100));
			EQ.Objs[iE].Destination = EditorGUILayout.Vector3Field("", EQ.Objs[EQ.CObjective].Destination);
			GUILayout.EndHorizontal();
			//COmpensation
			GUILayout.BeginHorizontal();
			GUILayout.Label("Compensation: ", GUILayout.Width(100));
			EQ.Objs[iE].MaxDist = EditorGUILayout.FloatField(EQ.Objs[EQ.CObjective].MaxDist);
			GUILayout.EndHorizontal();
			//Position to go to
			GUILayout.BeginHorizontal();
			GUILayout.Label("Object: ", GUILayout.Width(100));
			EQ.Objs[iE].ObjToMove = (GameObject)EditorGUILayout.ObjectField(EQ.Objs[EQ.CObjective].ObjToMove, typeof(GameObject), true);
			GUILayout.EndHorizontal();
		}
	}
}