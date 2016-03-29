using UnityEngine;
using System.Collections;

public class HeroStats : MonoBehaviour {

    public int currentLevel;
    public int currentExp;
    public int[] toLevelUp;
    public int[] HPLeveles;
    public int[] attackLevels;
    public int[] defenceLevels;
    public int currentHP;
    public int currentAttack;
    public int currentDefence;

    private HeroHealthManager heroHealth;

	// Use this for initialization
	void Start ()
    {
        currentHP = HPLeveles[1];
        currentAttack = attackLevels[1];
        currentDefence = defenceLevels[1];
        heroHealth = FindObjectOfType<HeroHealthManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(currentExp >= toLevelUp[currentLevel])
        {
            //currentLevel++;
            LevelUp();
        }
	}

    public void AddExperience(int experienceToAdd)
    {
        currentExp += experienceToAdd;
    }

    public void LevelUp()
    {
        currentLevel++;

        currentHP = HPLeveles[currentLevel];
        heroHealth.heroMaxHealth = currentHP;
        heroHealth.heroCurrentHealth += currentHP = HPLeveles[currentLevel - 1];

        currentAttack = attackLevels[currentLevel];
        currentDefence = defenceLevels[currentLevel];

    }
}
