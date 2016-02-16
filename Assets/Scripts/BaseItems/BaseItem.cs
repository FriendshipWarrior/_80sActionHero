using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseItem
{
    private string _itemName;
    private string _itemDescription;
    private int _itemID;
    private List<BaseStat> _stats;
    private ItemTypes _type;

    public enum ItemTypes
    {
        EQUIPMENT,
        WEAPON,
        POTION,
        KEY
    }

    public BaseItem() { }

    public BaseItem(Dictionary<string, string> itemsDictionary)
    {
        ItemName = itemsDictionary["ItemName"];
        ItemID = int.Parse(itemsDictionary["ItemID"]);
        ItemType = (ItemTypes)System.Enum.Parse(typeof(BaseItem.ItemTypes), itemsDictionary["ItemType"].ToString());
        ItemStats = new List<BaseStat>();
        ItemStats.Add(new BaseStamina());
        ItemStats.Add(new BaseStrength());
        ItemStats.Add(new BaseIntelligence());
        ItemStats.Add(new BaseLuck());
    }

    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    private int ItemID { get; set; }
    public List<BaseStat> ItemStats { get; set; }
    private ItemTypes ItemType { get; set; }
}
