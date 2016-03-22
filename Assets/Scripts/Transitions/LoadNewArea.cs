using UnityEngine;
using System.Collections;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Hero")
        {
            Application.LoadLevel(levelToLoad);
        }
    }
}
