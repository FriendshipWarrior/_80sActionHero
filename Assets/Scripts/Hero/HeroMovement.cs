using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;

    public bool canMove;
    public float attackTime;

    private static bool heroExists;
    private bool isWalking;
    private bool isAttacking;
    private float attackTimeCounter;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (!heroExists)
        {
            heroExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }     
    }

	// Update is called once per frame
	void FixedUpdate () {

        if (!isAttacking)
        {
            Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (!canMove)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                attackTimeCounter = attackTime;
                isWalking = false;
                isAttacking = true;
                rbody.velocity = Vector2.zero;
                anim.SetBool("is_attacking", true);
            }

            if (movement_vector != Vector2.zero && isWalking == true)
            {
                anim.SetBool("is_walking", true);
                anim.SetFloat("input_x", movement_vector.x);
                anim.SetFloat("input_y", movement_vector.y);
            }
            else
            {
                anim.SetBool("is_walking", false);
            }
            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);
        }

        if(attackTimeCounter >= 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if(attackTimeCounter <= 0)
        {
            isAttacking = false;
            isWalking = true;
            anim.SetBool("is_attacking", false);
        }
	}
}
