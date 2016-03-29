using UnityEngine;
using System.Collections;

public class HurtHero : MonoBehaviour {

    public int damageToGive;
    public GameObject damageNumber;

    private HeroStats heroStats;
    private int currentDamage;

	// Use this for initialization
	void Start ()
    {
        heroStats = FindObjectOfType<HeroStats>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            currentDamage = damageToGive - heroStats.currentDefence;
            if(currentDamage <= 0)
            {
                currentDamage = 1;
            }

            other.gameObject.GetComponent<HeroHealthManager>().HurtPlayer(currentDamage);
            var clone = (GameObject)Instantiate(damageNumber, other.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<FloatingNumbers>().damageNumber = currentDamage;
        }
    }
}
