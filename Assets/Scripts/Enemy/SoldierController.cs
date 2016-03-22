using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

    public float moveSpeed;
    public float timeBetweenMove;
    public float timeToMove;
    public float waitToReload;

    private Rigidbody2D rbody;
    private bool moving;
    private Vector3 moveDirection;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    private bool reloading;
    private GameObject hero;
    private Animator anim;

    // Use this for initialization
    void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //timeBetweenMoveCounter = timeBetweenMove;
        //timeToMoveCounter = timeToMove;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moving)
        {
            timeToMoveCounter -= Time.deltaTime;
            rbody.velocity = moveDirection;

            if(timeToMoveCounter < 0f)
            {
                moving = false;
                anim.SetBool("isWalking", false);
                //timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
            }
        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
            rbody.velocity = Vector2.zero;

            if(timeBetweenMoveCounter < 0f)
            {
                moving = true;
                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);
                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed,0f);
                if (moveDirection != Vector3.zero)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetFloat("input_x", moveDirection.x);
                    anim.SetFloat("input_y", moveDirection.y);
                }
            }
        }
        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if(waitToReload < 0)
            {
                Application.LoadLevel(Application.loadedLevel);
                hero.SetActive(true);
            }
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.gameObject.name == "Hero")
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            reloading = true;
            hero = other.gameObject;
        }
        */
    }
}
