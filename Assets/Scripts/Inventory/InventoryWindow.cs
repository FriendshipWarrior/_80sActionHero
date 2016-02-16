using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryWindow : MonoBehaviour {

    public int startingPosX;
    public int startingPosY;
    public int slotCntPerPage;
    public int slotCntLength;
    public GameObject itemSlotPrefab;
    public ToggleGroup itemSlotToggleGroup;

    private int xPos;
    private int yPos;
    private GameObject itemSlot;
    private int itemSlotCnt;

	// Use this for initialization
	void Start () {
        CreateInvetorySlotsInWindow();

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    private void CreateInvetorySlotsInWindow()
    {
        xPos = startingPosX;
        yPos = startingPosY;
        for(int i = 0; i < slotCntPerPage; i++)
        {
            itemSlot = (GameObject)Instantiate(itemSlotPrefab);
            itemSlot.name = "Empty";
            itemSlot.GetComponent<Toggle>().group = itemSlotToggleGroup;
            itemSlot.transform.SetParent(this.gameObject.transform);
            itemSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
            xPos += (int)itemSlot.GetComponent<RectTransform>().rect.width;
            itemSlotCnt++;
            if(itemSlotCnt % slotCntLength == 0)
            {
                itemSlotCnt = 0;
                yPos -= (int)itemSlot.GetComponent<RectTransform>().rect.width;
                xPos = startingPosX;
            }
        }
    }
}
