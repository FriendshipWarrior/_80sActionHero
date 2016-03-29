using UnityEngine;
using System.Collections;

public class Empress : MonoBehaviour {

    private GameObject weapon;
    private GameObject hero;
    private Inventory inv;
    private bool empressTalk;

    // Use this for initialization
    void Start ()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        hero = GameObject.Find("Hero");
        weapon = hero.transform.FindChild("Weapon").gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HeroMovement>() == null)
            return;
        inv.RemoveItem(0);
        inv.AddItem(0);
        weapon.SetActive(true);
        Destroy(this);
    }
}
