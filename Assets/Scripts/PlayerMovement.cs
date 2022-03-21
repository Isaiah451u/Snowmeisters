using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 ropeHook;
    public float swingForce = 10f;
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public bool groundCheck;
    public bool isSwinging;
    public LayerMask groundLayer;
    public Transform groundCheckObject;
    private SpriteRenderer playerSprite;
    public Rigidbody2D rBody;
    private bool isJumping;
    private Animator animator;
    private float jumpInput;
    private float horizontalInput;
    private bool candoubleJump;

    public AudioClip walking;
    public AudioClip jump;
    private AudioSource audioSource;
    public AudioSource gameManager;

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<BoxCollider2D>().bounds.extents.y;
        groundCheck = Physics2D.OverlapCircle(groundCheckObject.position, 0.15f, groundLayer);


        if (isSwinging)
        {
            rBody.drag = .15f;
        }
        else if (isSwinging == false && groundCheck)
        {
            rBody.drag = 2.7f;
        }

        if (!isSwinging)
        {
            if (groundCheck)
            {
                candoubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                

                if (groundCheck)
                {
                    rBody.velocity = Vector2.up * jumpSpeed;
                    gameManager.PlayOneShot(jump);
                }
                else
                {
                    if (candoubleJump)
                    {
                        rBody.velocity = Vector2.up * jumpSpeed;
                        gameManager.PlayOneShot(jump);
                        candoubleJump = false;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (groundCheck)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;

            if (isSwinging)
            {

                // 1 - Get a normalized direction vector from the player to the hook point
                var playerToHookDirection = (ropeHook - (Vector2)transform.position).normalized;

                // 2 - Inverse the direction to get a perpendicular direction
                Vector2 perpendicularDirection;
                if (horizontalInput < 0)
                {
                    perpendicularDirection = new Vector2(-playerToHookDirection.y, playerToHookDirection.x);
                    var leftPerpPos = (Vector2)transform.position - perpendicularDirection * -2f;
                    Debug.DrawLine(transform.position, leftPerpPos, Color.green, 0f);
                }
                else
                {
                    perpendicularDirection = new Vector2(playerToHookDirection.y, -playerToHookDirection.x);
                    var rightPerpPos = (Vector2)transform.position + perpendicularDirection * 2f;
                    Debug.DrawLine(transform.position, rightPerpPos, Color.green, 0f);
                }

                var force = perpendicularDirection * swingForce;
                rBody.AddForce(force, ForceMode2D.Force);
            }
            else
            {
                if (horizontalInput < 0f || horizontalInput > 0f)
                {
                    playerSprite.flipX = horizontalInput < 0f;
                    audioSource.UnPause();
                    var groundForce = speed * 2f;
                    rBody.AddForce(new Vector2((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                    rBody.velocity = new Vector2(Mathf.Clamp(rBody.velocity.x, -10, 10), rBody.velocity.y);
                }
            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            audioSource.Pause(); 
        }

       
    }
}
