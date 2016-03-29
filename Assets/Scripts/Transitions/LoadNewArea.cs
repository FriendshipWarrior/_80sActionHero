using UnityEngine;
using System.Collections;

public class LoadNewArea : MonoBehaviour {

    public string levelToLoad;
    public string exitPoint;

    private HeroMovement hero;

    void Start()
    {
        hero = FindObjectOfType<HeroMovement>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Hero")
        {
            Application.LoadLevel(levelToLoad);
            hero.startPoint = exitPoint;
        }
    }
}
