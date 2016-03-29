using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {
    public Item item;
    public HeroHealthManager hero;
    public int amount;
    public int slot;

    //private CharacterPanel characterPanel;
    private Inventory inv;
    private ToolTip tooltip;
    private Vector2 offset;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        hero = GameObject.Find("Hero").GetComponent<HeroHealthManager>();
        //characterPanel = GameObject.Find("Character Panel").GetComponent<CharacterPanel>();
        tooltip = inv.GetComponent<ToolTip>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inv.slots[slot].transform);
        this.transform.position = inv.slots[slot].transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -2)
        {
            if (item.Type == "Weapon" || item.Type == "Armor")
            {
                int uniqueId = GameObject.Find("Slot Panel").transform.GetChild(slot).transform.GetChild(0).GetInstanceID();

                //EquipItem(item, uniqueId);
                tooltip.Deactivate();
            }
            if (item.Consumable)
            {
                switch (item.ID)
                {
                    case 1:
                        hero.heroCurrentHealth += 50;
                        break;
                }
                inv.RemoveItem(item.ID);
                /*
                if (inv.RemoveItem(item.ID) == 0)
                    tooltip.Deactivate();
                */
            }
        }
    }
}
