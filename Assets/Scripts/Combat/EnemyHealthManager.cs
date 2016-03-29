using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;

    private HeroStats hero;

    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        hero = FindObjectOfType<HeroStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
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
