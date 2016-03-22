using UnityEngine;
using System.Collections;

public class HurtEnemy : MonoBehaviour {

    public int damageToGive;
    public GameObject damageBurst;
    public Transform hitPoint;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Soldier")
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
        }
    }
}
