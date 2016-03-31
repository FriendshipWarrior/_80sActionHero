using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour
{
    public int itemID;

    private Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HeroMovement>() == null)
        {
            return;
        }

        inv.AddItem(itemID);
        Destroy(gameObject);     
    }
    /*
    public Inventory inv;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bool addedItem = inv.AddItem(itemID);

            if (addedItem)
                Destroy(gameObject);
            else
                Debug.Log("Inventory is full");
        }
    }
    */
}
