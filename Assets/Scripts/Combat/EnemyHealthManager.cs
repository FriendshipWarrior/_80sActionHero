using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;

    private HeroStats hero;
    private QuestManager manager;
    private List<Quest> Q = new List<Quest>();

    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        hero = FindObjectOfType<HeroStats>();
        manager = (QuestManager)FindObjectOfType(typeof(QuestManager));
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            //Get all the quest we have
            Q = manager.QList;
            //Iterate through them
            for (int i = 0; i < Q.Count; i++)
            {
                //Iterate through there objectives
                for (int s = 0; s < Q[i].AmountOfO; s++)
                    //Check if the quest is the right one
                    if (Q[i].CurrentQuest && Q[i].Objs[s].qType == QuestType.Defeat)
                    {
                        //Make sure the objective isn't completed	
                        if (Q[i].Objs[s].Amount < Q[i].Objs[s].AmountNeeded)
                        {
                            //Check to see if it's the right object
                            if (gameObject.name == Q[i].Objs[s].Name)
                            {
                                //Make sure we're in the right world for the quest
                                if (Application.loadedLevelName == Q[i].Objs[s].World || Q[i].Objs[s].World == "")
                                {
                                    //Add to the amount
                                    Q[i].Objs[s].Amount += 1;
                                }
                            }
                        }
                    }
            }
            Destroy(gameObject);
            hero.AddExperience(expToGive);
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        enemyCurrentHealth -= damageToGive;
    }

    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
}
