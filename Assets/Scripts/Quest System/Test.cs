using UnityEngine;
using System.Collections;
using QuestSystem;

public class Test : MonoBehaviour {

    public GameObject conanVHS;

	// Use this for initialization
	void Start () {
        IQuestObjective qb = new CollectionObjective("Get VHS", 1, conanVHS, "Pick up the Conana VHS",false);
        Debug.Log(qb.ToString());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
