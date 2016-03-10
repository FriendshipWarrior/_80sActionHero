using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToolTip : MonoBehaviour {

    private Item item;
    private string data;
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("ToolTip");
        tooltip.SetActive(false);
    }

    void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item)
    {
        this.item = item;
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        data = "<color=#0473f0>" + item.Title + "</color>\n" + 
            item.Description;
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }
}
