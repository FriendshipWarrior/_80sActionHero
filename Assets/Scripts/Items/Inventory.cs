using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    public static bool invExists;


    private int slotAmount;

    void Start()
    {
        if (!invExists)
        {
            invExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        database = GetComponent<ItemDatabase>();
        slotAmount = 24;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i < slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);
        }
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
    }

    public bool AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        if (itemToAdd.Stackable && ItemCheck(itemToAdd) != -1)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    return true;
                }
            }
            return false;
        }
        else {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.transform.position = slots[i].transform.position;
                    //itemObj.transform.position = Vector2.zero;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    itemObj.name = itemToAdd.Title;
                    return true;
                }
            }
            return false;
        }
    }
    /*
    bool CheckIfItemIsInInventory(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == item.ID)
            {
                return true;
            }
        }
        return false;
    }
    */
    private int RemoveAtPos(int pos, Item itemToRemove)
    {
        //Item itemToRemove = database.FetchItemByID(id);
        //int pos = ItemCheck(itemToRemove);
        if (pos != -1)
        {
            if (items[pos].Stackable)
            {
                ItemData data = slots[pos].transform.GetComponentInChildren<ItemData>();
                data.amount--;
                if (data.amount == 0)
                {
                    items[pos] = new Item();
                    Transform t = slots[pos].transform.GetChild(0);
                    Destroy(t.gameObject);
                    return 0;

                }
                else
                {
                    if (data.amount == 1)
                        data.transform.GetComponentInChildren<Text>().text = "";
                    else
                        data.transform.GetComponentInChildren<Text>().text = data.amount.ToString();
                    return data.amount;
                }
            }
            else
            {
                items[pos] = new Item();
                Transform t = slots[pos].transform.GetChild(0);
                Destroy(t.gameObject);
                return 0;
            }
        }
        return -1;
    }

    public int RemoveItem(int id)
    {
        Item itemToRemove = database.FetchItemByID(id);
        int pos = ItemCheck(itemToRemove);
        return (RemoveAtPos(pos, itemToRemove));
    }

    public int RemoveUniqueItem(int uniqueId, int itemId)
    {
        Item itemToRemove = database.FetchItemByID(itemId);
        int pos = UniqueItemCheck(uniqueId);
        return (RemoveAtPos(pos, itemToRemove));
    }

    int UniqueItemCheck(int id)
    {
        GameObject invSlots = GameObject.Find("Slot Panel");
        foreach (Transform child in invSlots.transform)
        {
            try
            {
                if (child.transform.GetChild(0).GetInstanceID() == id)
                    return child.GetComponent<Slot>().id;
            }
            catch
            { }
        }
        return -1;
    }

    int ItemCheck(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return i;
        }
        return -1;
    }
}
