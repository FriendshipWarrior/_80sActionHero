using UnityEngine;
using System.Collections;

public class MusicSwitch : MonoBehaviour {

    public GameObject sceneMusic;
    public GameObject bossMusic;

	// Use this for initialization
	void Start ()
    {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HeroMovement>() == null)
        {
            return;
        }
        sceneMusic.SetActive(false);
        bossMusic.SetActive(true);
    }
}
