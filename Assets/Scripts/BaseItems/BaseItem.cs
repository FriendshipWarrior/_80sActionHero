using UnityEngine;
using System.Collections;

public class BaseItem
{
    private string itemName;
    private string itemDescription;
    private int itemID;
    private int stamina;
    private int strength;
    private int intelligence;
    public enum ItemTypes
    {
        EQUIPMENT,
        WEAPON,
        POTION,
        KEY
    }
    private ItemTypes itemType;

    public string ItemName
    {
        get;
        set;
    }
    public string ItemDescription
    {
        get;
        set;
    }
    private int ItemID
    {
        get;
        set;
    }
    private int Stamina
    {
        get;
        set;
    }
    private int Strength
    {
        get;
        set;
    }
    private int Intelligence
    {
        get;
        set;
    }
    private ItemTypes ItemType
    {
        get;
        set;
    }
}
