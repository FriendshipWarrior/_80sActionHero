using UnityEngine;
using System.Collections;

public class HeroHealthManager : MonoBehaviour {

    public int heroMaxHealth;
    public int heroCurrentHealth;
    public float flashLength;

    private bool flashActive;
    private float flashCounter;
    private SpriteRenderer heroSprite;

	// Use this for initialization
	void Start ()
    {
        heroCurrentHealth = heroMaxHealth;
        heroSprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(heroCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
        }

        if (flashActive)
        {
            if(flashCounter > flashLength * .66f)
            {
                heroSprite.color = new Color(heroSprite.color.r, heroSprite.color.g, heroSprite.color.b, 0);
            }
            else if(flashCounter > flashLength * .33f)
            {
                heroSprite.color = new Color(heroSprite.color.r, heroSprite.color.g, heroSprite.color.b, 1f);
            }
            else if(flashCounter > 0f)
            {
                heroSprite.color = new Color(heroSprite.color.r, heroSprite.color.g, heroSprite.color.b, 0);
            }
            else
            {
                heroSprite.color = new Color(heroSprite.color.r, heroSprite.color.g, heroSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
        
        if(heroCurrentHealth > heroMaxHealth)
        {
            heroCurrentHealth = heroMaxHealth;
        }
	}

    public void HurtPlayer(int damageToGive)
    {
        heroCurrentHealth -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;
    }

    public void SetMaxHealth()
    {
        heroCurrentHealth = heroMaxHealth;
    }
}
