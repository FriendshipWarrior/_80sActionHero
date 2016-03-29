using UnityEngine;
using System.Collections;

public class EventSystem : MonoBehaviour {

    public static bool esExists;

	// Use this for initialization
	void Start ()
    {
        if (!esExists)
        {
            esExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
