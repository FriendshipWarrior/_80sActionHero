using UnityEngine;
using System.Collections;

public class HeroMovement : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;

    public bool canMove;
    public float moveSpeed;
    public float attackTime;
    public string startPoint;
    public float diagonalMoveModifier;

    private static bool heroExists;
    private bool isWalking;
    private bool isAttacking;
    private float attackTimeCounter;
    private float currentMoveSpeed;

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
                anim.SetBool("is_walking", false);
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

            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
            {
                currentMoveSpeed = moveSpeed * diagonalMoveModifier;
            }
            else
            {
                currentMoveSpeed = moveSpeed;
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
            rbody.MovePosition(rbody.position + movement_vector * (currentMoveSpeed * Time.deltaTime));//Time.deltaTime);
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
