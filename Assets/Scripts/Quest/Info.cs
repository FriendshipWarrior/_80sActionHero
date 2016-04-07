using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

	private Vector3 scale;
	public float originalWidth = 1280.0f;
	public float originalHeight = 720.0f;

	public Rect Pos;

	void OnGUI(){
		scale.x = Screen.width / originalWidth;
		scale.y = Screen.height / originalHeight;
		scale.z = 1.0f;
		var svMat = GUI.matrix;
		GUI.matrix = Matrix4x4.TRS(new Vector3(0,0,0), Quaternion.identity, scale);
		GUI.color = Color.green;

		GUILayout.BeginArea (Pos);

		GUILayout.BeginVertical ();
		GUILayout.Label ("Click the NPC to get a mission");
		GUILayout.EndVertical ();

		GUILayout.EndArea ();

		GUI.matrix = svMat; // restore matrix
	}
}
