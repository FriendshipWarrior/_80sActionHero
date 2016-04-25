using UnityEngine;
using System.Collections;

public class Princess : MonoBehaviour {

    private Inventory inv;
    private QuestGiver qg;
    private QuestGiver king;
    private MusicSwitch musicSwitch;

    public GameObject sceneMusic;
    public GameObject bossMusic;
    public static bool princessTalk;
    // Use this for initialization
    void Start ()
    {
        king = GameObject.FindGameObjectWithTag("King").GetComponent<QuestGiver>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        qg = GetComponent<QuestGiver>();
        musicSwitch = GameObject.FindObjectOfType<MusicSwitch>();
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
            king.enabled = false;
            qg.enabled = true;
            princessTalk = true;
            bossMusic.SetActive(false);
            sceneMusic.SetActive(true);
            Destroy(musicSwitch);
        }
    }
}
