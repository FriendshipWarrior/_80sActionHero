﻿using UnityEngine;
using System.Collections;

//Not really needed atm
public class XPManager : MonoBehaviour {

	private int XP = 0;
	public int MaxXP = 100;

	//GUI
	private Vector3 scale;
	public float originalWidth = 1280.0f;
	public float originalHeight = 720.0f;

	public Rect Pos;
	public GUISkin skin;

	void OnGUI(){
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1.0f;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0,0,0), Quaternion.identity, scale);
		GUI.skin = skin;

		GUILayout.BeginArea (Pos);

		GUILayout.Label ("XP: " + XP.ToString() + "/" + MaxXP.ToString());

		GUILayout.EndArea ();

		GUI.matrix = svMat; // restore matrix
	}
}
