using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour {

    public GameObject levelSelect;
    public HeroMovement hero;
    public bool isActive;
    public bool stopHeroMovement;

    // Use this for initialization
    void Start ()
    {
        hero = FindObjectOfType<HeroMovement>();
        if (isActive)
        {
            EnableLevelPanel();
        }
        else
        {
            DisableLevelPanel();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isActive)
        {
            return;
        }
    }
    public void EnableLevelPanel()
    {
        levelSelect.SetActive(true);
        isActive = true;
        if (stopHeroMovement)
        {
            hero.canMove = false;
        }
    }
    public void DisableLevelPanel()
    {
        levelSelect.SetActive(false);
        isActive = false;
        hero.canMove = true;
    }
}
