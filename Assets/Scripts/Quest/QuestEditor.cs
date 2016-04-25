using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor {

    public GameObject hero;
	public Quest Q;
	
	public override void OnInspectorGUI(){
		Q = target as Quest;
		EditorStyles.textField.wordWrap = true;
		
		//Quest Name
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Quest Name: ", GUILayout.Width(85));
        Q.QuestName = EditorGUILayout.TextField(Q.QuestName, GUILayout.Width(225));
        GUILayout.EndHorizontal ();
		//Quest ID
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Overall ID: ", GUILayout.Width(85));
		Q.OverallID = int.Parse(GUILayout.TextField (Q.OverallID.ToString(), GUILayout.Width(125)));
		GUILayout.EndHorizontal ();
		//Progression ID
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Progress ID: ", GUILayout.Width(85));
		Q.ProgressionID = int.Parse(GUILayout.TextField (Q.ProgressionID.ToString(), GUILayout.Width(125)));
		GUILayout.EndHorizontal ();
		//Quest ID
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Quest ID: ", GUILayout.Width(85));
		Q.QuestID = int.Parse(GUILayout.TextField (Q.QuestID.ToString(), GUILayout.Width(125)));
		GUILayout.EndHorizontal ();
		//Quest Description
		GUILayout.BeginVertical ();
		GUILayout.Label("Quest Description: ", GUILayout.Width(125));
		Q.Description = EditorGUILayout.TextField (Q.Description, GUILayout.Height(80));
		GUILayout.EndVertical ();
		//Complete Quest
		GUILayout.BeginVertical ();
		GUILayout.Label("Complete Quest Message: ", GUILayout.Width(175));
		Q.CompleteQ = EditorGUILayout.TextField (Q.CompleteQ, GUILayout.Height(20));
		GUILayout.EndVertical ();
		//hero(s)
		GUILayout.BeginHorizontal ();
		GUILayout.Label("hero Tag: ", GUILayout.Width(85));
		Q.heroT = EditorGUILayout.TextField (Q.heroT, GUILayout.Width(125));
		GUILayout.EndHorizontal ();
        //Exp
        GUILayout.BeginHorizontal();
        GUILayout.Label("Exp to Give: ", GUILayout.Width(85));
        Q.expToGive = int.Parse(GUILayout.TextField(Q.expToGive.ToString(), GUILayout.Width(125)));
        GUILayout.EndHorizontal();
        //Add/Remove Objectives
        GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Add Objective")) {
			Q.AmountOfO += 1;
		}
		if (GUILayout.Button ("Remove Objective")) {
			Q.AmountOfO -= 1;
		}
		GUILayout.EndHorizontal();
		
		
		if(Q.Objs.Count < Q.AmountOfO){
			Q.Objs.Add(new ObjectiveT());
		}
		for(int i = 0; i < Q.AmountOfO; i++){
			//Quest type
			GUILayout.BeginHorizontal ();
			GUILayout.Label("Quest Type: ", GUILayout.Width(85));
			Q.Objs[i].qType = (QuestType)EditorGUILayout.EnumPopup(Q.Objs[i].qType);
			GUILayout.EndHorizontal ();
			//World Name
			GUILayout.BeginHorizontal();
			GUILayout.Label("World: ");
			Q.Objs[i].World = EditorGUILayout.TextField(Q.Objs[i].World);
			GUILayout.EndHorizontal();
			//Short Description
			GUILayout.BeginHorizontal();
			GUILayout.Label("Short Summary: ", GUILayout.Width(100));
			Q.Objs[i].sDescript = EditorGUILayout.TextField(Q.Objs[i].sDescript);
			GUILayout.EndHorizontal();
			GUILayout.Space(2);
			
			
			ShowQuestOpt (i);
			GUILayout.Space(14);
		}
	}
	
	void ShowQuestOpt(int iE){
		if(Q.Objs[iE].qType == QuestType.GoTo){
			if(Q.Objs.Count < Q.AmountOfO){
				Q.Objs.Add(new ObjectiveT());
			}else{
				//Position to go to
				GUILayout.BeginHorizontal();
				GUILayout.Label("Position: ", GUILayout.Width(100));
				Q.Objs[iE].Destination = EditorGUILayout.Vector3Field("", Q.Objs[iE].Destination);
				GUILayout.EndHorizontal();
				GUILayout.Space(2);
				//COmpensation
				GUILayout.BeginHorizontal();
				GUILayout.Label("Compensation: ", GUILayout.Width(100));
				Q.Objs[iE].MaxDist = EditorGUILayout.FloatField(Q.Objs[iE].MaxDist);
				GUILayout.EndHorizontal();
			}
		}
		if (Q.Objs[iE].qType == QuestType.Collect || Q.Objs[iE].qType == QuestType.Defeat) {
			//Item Name
			GUILayout.BeginHorizontal();
			if(Q.Objs[iE].qType == QuestType.Collect){
				GUILayout.Label("Item Name: ", GUILayout.Width(100));
			}
			if(Q.Objs[iE].qType == QuestType.Defeat){
				GUILayout.Label("Enemy Name: ", GUILayout.Width(100));
			}
			Q.Objs[iE].Name = EditorGUILayout.TextField(Q.Objs[iE].Name);
			GUILayout.EndHorizontal();
			//Amount Needed
			GUILayout.BeginHorizontal();
			GUILayout.Label("Amount Needed: ", GUILayout.Width(100));
			Q.Objs[iE].AmountNeeded = int.Parse(EditorGUILayout.TextField(Q.Objs[Q.CObjective].AmountNeeded.ToString()));
			GUILayout.EndHorizontal();
		}
		if (Q.Objs[iE].qType == QuestType.MoveObj) {
			//Position to go to
			GUILayout.BeginHorizontal();
			GUILayout.Label("Destination: ", GUILayout.Width(100));
			Q.Objs[iE].Destination = EditorGUILayout.Vector3Field("", Q.Objs[Q.CObjective].Destination);
			GUILayout.EndHorizontal();
			//COmpensation
			GUILayout.BeginHorizontal();
			GUILayout.Label("Compensation: ", GUILayout.Width(100));
			Q.Objs[iE].MaxDist = EditorGUILayout.FloatField(Q.Objs[Q.CObjective].MaxDist);
			GUILayout.EndHorizontal();
			//Position to go to
			GUILayout.BeginHorizontal();
			GUILayout.Label("Object: ", GUILayout.Width(100));
			Q.Objs[iE].ObjToMove = (GameObject)EditorGUILayout.ObjectField(Q.Objs[Q.CObjective].ObjToMove, typeof(GameObject), true);
			GUILayout.EndHorizontal();
		}
	}
}