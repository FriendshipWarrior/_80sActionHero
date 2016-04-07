using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {
    public Transform target;
    public float speed = 1;
    public float followDistance = 10;
    public float attackDistance = 2;
    public Animator anim;

    private GameObject hero;
    private float range;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
        hero = GameObject.FindGameObjectWithTag("Hero");
    }
    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("is_attacking"))
        {
            if (hero.transform.position.x > transform.position.x)
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
        }
        range = Vector2.Distance(transform.position, hero.transform.position);
        if (range <= attackDistance)
        {
            anim.SetBool("is_attacking", true);
            anim.SetBool("is_walking", false);
        }
        else if (range <= followDistance)
        {
            anim.SetBool("is_attacking", false);
            anim.SetBool("is_walking", true);
            transform.position += (hero.transform.position - transform.position).normalized * speed * Time.deltaTime;
        }
        else {
            anim.SetBool("is_attacking", false);
            anim.SetBool("is_walking", false);
        }
    }
}
