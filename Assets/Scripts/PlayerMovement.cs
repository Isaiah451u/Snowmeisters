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
    private Rigidbody2D rBody;
    private bool isJumping;
    private Animator animator;
    private float jumpInput;
    private float horizontalInput;
    

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        jumpInput = Input.GetAxis("Jump");
        horizontalInput = Input.GetAxis("Horizontal");
        var halfHeight = transform.GetComponent<BoxCollider2D>().bounds.extents.y;
        groundCheck = Physics2D.OverlapCircle(groundCheckObject.position, 0.15f, groundLayer);

    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            //animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;
            if (isSwinging)
            {
                //animator.SetBool("IsSwinging", true);

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
                //animator.SetBool("IsSwinging", false);
                if (horizontalInput < 0f || horizontalInput > 0f)
                {
                    playerSprite.flipX = horizontalInput < 0f;

                    var groundForce = speed * 2f;
                    rBody.AddForce(new Vector2((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                    rBody.velocity = new Vector2(Mathf.Clamp(rBody.velocity.x, -10, 10), rBody.velocity.y);
                }
            }
        }
        else
        {
            //animator.SetBool("IsSwinging", false);
            //animator.SetFloat("Speed", 0f);
        }

        if (!isSwinging)
        {
            if (!groundCheck) return;

            isJumping = jumpInput > 0f;
            if (isJumping)
            {
                rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
            }
        }
    }

}
