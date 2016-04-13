using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Slider healthBar;
    public Text hp;
    public Text EXP;
    public Text QuestDescrip;
    public Text QuestObj;
    public HeroHealthManager hero;
    public static bool UIExists;

    private HeroStats heroStats;
    private QuestManager manager;
    private bool showCompleted = false;

    // Use this for initialization
    void Start ()
    {
        if (!UIExists)
        {
            UIExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        heroStats = GetComponent<HeroStats>();
        manager = GameObject.FindGameObjectWithTag("QManager").GetComponent<QuestManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        healthBar.maxValue = hero.heroMaxHealth;
        healthBar.value = hero.heroCurrentHealth;
        hp.text = "HP:  " + hero.heroCurrentHealth + "/" + hero.heroMaxHealth;
        EXP.text = "Lvl: " + heroStats.currentLevel + " " + "EXP: " + heroStats.currentExp + "/" + heroStats.toLevelUp[heroStats.currentLevel];
        if (manager.CurrentQuest != null)
        {
            QuestDescrip.text = manager.CurrentQuest.QuestName + ":";

            if (manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].qType == QuestType.GoTo)
            {
                QuestObj.text = manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].sDescript;
            }
            if (manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].qType == QuestType.Collect)
            {
                QuestObj.text = manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].sDescript + "\n" + "\n" +
                 + manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].Amount + "/" 
                            + manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].AmountNeeded;
            }
            if (manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].qType == QuestType.Defeat)
            {
                QuestObj.text = manager.CurrentQuest.Objs[manager.CurrentQuest.CObjective].sDescript;
            }
        }
        if (manager.CurrentQuest == null)
        {
            QuestDescrip.text = "";
            QuestObj.text = "";
        }
    }

    public void OnClickYes()
    {

    }
}
