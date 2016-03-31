using UnityEngine;
using System.Collections;
using QuestSystem;

public class Test : MonoBehaviour {

    public GameObject item;

	// Use this for initialization
	void Start () {
        IQuestObjective qb = new CollectionObjective("Get VHS", 1, item, "Pick up the Conana VHS",false);
        Debug.Log(qb.ToString());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
