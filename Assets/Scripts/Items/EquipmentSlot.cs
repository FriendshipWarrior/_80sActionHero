using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EquipmentSlot : MonoBehaviour {//, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler{

    public string equipmentType;
    public Item equippedItem = null;
    /*
    Inventory inv;
    ToolTip tooltip;
    
    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<ToolTip>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            if (inv.AddItem(equippedItem.ID))
            {
                //Hides the item icon so it doesn't draw over the slot icon 
                transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, 0);

                //Ignore this if you aren't worried about an example on how to change stats
                /*
                stats.additionalAttackPower -= equippedItem.Power;
                stats.additionalAttackSpeed -= equippedItem.Speed;
                int totalAttack = stats.additionalAttackPower + stats.baseAttackPower;
                int totalSpeed = stats.additionalAttackSpeed + stats.baseAttackSpeed;
                transform.parent.transform.GetChild(0).GetComponent<Text>().text = "Power - " + totalAttack + "\nSpeed - " + totalSpeed;
                
                //Deactive the tooltip since the item is now gone
                tooltip.Deactivate();
                //Clear the equipped item slot
                equippedItem = new Item();
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedItem.ID != -1)
            tooltip.Activate(equippedItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
*/
}
