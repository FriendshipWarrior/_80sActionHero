using UnityEngine;
using System.Collections;

public class HeroHealthManager : MonoBehaviour {

    public int heroMaxHealth;
    public int heroCurrentHealth;

	// Use this for initialization
	void Start ()
    {
        heroCurrentHealth = heroMaxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(heroCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
	}

    public void HurtPlayer(int damageToGive)
    {
        heroCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        heroCurrentHealth = heroMaxHealth;
    }
}
