using UnityEngine;
using System.Collections;

public class CheapBossFight : MonoBehaviour
{

    public float moveSpeed;
    public float followDistance;
    public float attakDistance;
    public float waitToReload;

    private Rigidbody2D rbody;
    private GameObject hero;
    public Transform target;
    private Animator anim;
    private Vector3 moveDirection;
    private float timeBetweenMoveCounter;
    private float timeToMoveCounter;
    private float range;
    private bool reloading;
    private bool moving;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Hero").transform;
        hero = GameObject.FindGameObjectWithTag("Hero");
    }
    // Update is called once per frame
    void Update()
    {
        rbody.velocity = moveDirection;
        range = Vector2.Distance(transform.position, target.transform.position);

        if (range <= followDistance)
        {
            moveDirection = new Vector3((target.transform.position.x - transform.position.x) * moveSpeed, (target.transform.position.y - transform.position.y) * moveSpeed, 0f);
            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("input_x", moveDirection.x);
                anim.SetFloat("input_y", moveDirection.y);
            }
        }
        else
        {
            anim.SetBool("isWalking", false);
            rbody.velocity = Vector2.zero;
        }
        if (reloading)
        {
            waitToReload -= Time.deltaTime;
            if (waitToReload < 0)
            {
                Application.LoadLevel(Application.loadedLevel);
                hero.SetActive(true);
            }
        }
    }
}
