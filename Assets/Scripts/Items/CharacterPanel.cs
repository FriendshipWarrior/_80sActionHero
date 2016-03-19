using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterPanel : MonoBehaviour {

    private Inventory inv;

    // Use this for initialization
    void Start () {
        inv = GameObject.Find("Inventory Panel").GetComponent<Inventory>();
    }

    public void EquipItem(Item newEquip, int uniqueId)
    {
        foreach (Transform child in transform)
        {
            try
            {
                if (child.GetComponent<EquipmentSlot>().equipmentType == newEquip.Type)
                {
                    EquipmentSlot equip = child.GetComponent<EquipmentSlot>();
                    //Checks if an item isn't already equipped
                    if (equip.equippedItem.ID == -1)
                    {
                        inv.RemoveUniqueItem(uniqueId, newEquip.ID);
                        equip.equippedItem = newEquip;
                    }
                    else
                    {
                        //Remove the equipping item from the inventory
                        inv.RemoveUniqueItem(uniqueId, newEquip.ID);
                        //Assign the current weapon to a holding variable
                        Item holder = equip.equippedItem;
                        //Adds the current item to the inventory
                        if (inv.AddItem(holder.ID))
                        {
                            equip.equippedItem = newEquip;
                        }
                        else
                        {
                            inv.AddItem(newEquip.ID);
                        }
                    }

                    child.GetChild(0).GetComponent<Image>().sprite = newEquip.Sprite;
                    child.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 1);

                    //Ignore this if you aren't worried about an example on how to change stats
                    /*
                    stats.additionalAttackPower = newEquip.Power;
                    stats.additionalAttackSpeed = newEquip.Speed;
                    int totalAttack = stats.additionalAttackPower + stats.baseAttackPower;
                    int totalSpeed = stats.additionalAttackSpeed + stats.baseAttackSpeed;
                    transform.GetChild(0).GetComponent<Text>().text = "Power - " + totalAttack + "\nSpeed - " + totalSpeed;
                    */
                }
            }
            catch { }
        }
    }
}
