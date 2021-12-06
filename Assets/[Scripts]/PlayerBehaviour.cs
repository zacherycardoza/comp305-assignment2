using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public PlayerState playerState;

    [Header("Movement")]
    public float horizontalForce;
    public float verticalForce;
    public bool isGrounded;
    public bool isJumping;
    public Transform GroundOrigin;
    public float GroundRadius;
    public LayerMask GroundLayerMask;
    public float yVelocityTopBound;

    [Header("Sound FX")]
    public AudioSource jumpSound;


    private Animator animatorController;
    private Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        jumpSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = 0.0f;
        float y = 0.0f;

        SetIsGrounded();
        if (isGrounded)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Jump");

            // check if player is moving
            if (x != 0)
            {
                x = FlipAnimation(x);

                // shift to run animation
                animatorController.SetInteger("AnimationState", 1); // run

                playerState = transform.localScale.x > 0 ? PlayerState.RUN_RIGHT : PlayerState.RUN_LEFT;
            }
            else
            {
                // shift back to idle
                animatorController.SetInteger("AnimationState", 0); // idle
                playerState = PlayerState.IDLE;
            }

            if ((y > 0) && (!isJumping))
            {
                jumpSound.Play();
                isJumping = true;

            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal") * 0.01f; // less input when falling or jumping

            if (x != 0)
            {
                x = FlipAnimation(x);
            }

            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x * 0.90f, rigidBody2D.velocity.y);

            animatorController.SetInteger("AnimationState", 2); // jump
            playerState = PlayerState.JUMPING;

        }

        Vector2 movementVector = new Vector2(x * horizontalForce, y * verticalForce);
        rigidBody2D.AddForce(movementVector);
        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, Mathf.Clamp(rigidBody2D.velocity.y, -40f, yVelocityTopBound));
    }

    private float FlipAnimation(float x)
    {
        // uses the Ternary operator
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    public void SetIsGrounded()
    {
        RaycastHit2D hit = Physics2D.CircleCast(GroundOrigin.position, GroundRadius, Vector2.down, GroundRadius, GroundLayerMask);

        isGrounded = (hit) ? true : false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireSphere(GroundOrigin.position, GroundRadius);
    //}


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }


}
