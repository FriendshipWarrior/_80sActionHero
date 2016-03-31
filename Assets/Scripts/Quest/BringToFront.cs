using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class BringToFront : MonoBehaviour {

	public EventSystem eventSystem; //Our current eventSystem.
	public GameObject DefaultButton; //The default button that will be selected.

	void OnEnable(){
		//Brings quest UI to the front of all current UI once it's enabled.
		//eventSystem.SetSelectedGameObject (DefaultButton);
		transform.SetAsLastSibling ();
	}
}
