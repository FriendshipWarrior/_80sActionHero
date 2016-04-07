using UnityEngine;
using System.Collections;

public class Princess : MonoBehaviour {

    private Inventory inv;
    public static bool princessTalk;
    // Use this for initialization
    void Start ()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
	
	// Update is called once per frame
	void Update () {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HeroMovement>() == null)
            return;
        if (!princessTalk)
        {
            inv.AddItem(200);
            inv.RemoveItem(1000);
            princessTalk = true;
        }
    }
}
