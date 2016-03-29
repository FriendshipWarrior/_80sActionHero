using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text hp;
    public Text EXP;
    public HeroHealthManager hero;
    public static bool UIExists;

    private HeroStats heroStats;

	// Use this for initialization
	void Start ()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        heroStats = GetComponent<HeroStats>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.maxValue = hero.heroMaxHealth;
        healthBar.value = hero.heroCurrentHealth;
        hp.text = "HP:  " + hero.heroCurrentHealth + "/" + hero.heroMaxHealth;
        EXP.text = "Lvl: " + heroStats.currentLevel + " " + "EXP: " + heroStats.currentExp + "/" + heroStats.toLevelUp[heroStats.currentLevel];
	}
}
