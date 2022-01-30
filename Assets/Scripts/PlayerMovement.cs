﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float jumpSpeed = 3f;
    public bool groundCheck;
    public bool isSwinging;
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
        groundCheck = Physics2D.Raycast(transform.position, -Vector3.up, halfHeight + 0.1f);
    }

    void FixedUpdate()
    {
        if (horizontalInput < 0f || horizontalInput > 0f)
        {
            //animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
            playerSprite.flipX = horizontalInput < 0f;

            if (groundCheck)
            {
                var groundForce = speed * 2f;
                rBody.AddForce(new Vector2((horizontalInput * groundForce - rBody.velocity.x) * groundForce, 0));
                rBody.velocity = new Vector2(Mathf.Clamp(rBody.velocity.x, -10, 10), rBody.velocity.y);
            }
        }
        else
        {
            //animator.SetFloat("Speed", 0f);
        }

        if (!groundCheck) return;

        isJumping = jumpInput > 0f;
        if (isJumping)
        {
            rBody.velocity = new Vector2(rBody.velocity.x, jumpSpeed);
        }
    }
}
