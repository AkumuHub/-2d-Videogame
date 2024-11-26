using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    float horizontalInput;
    float moveSpeed = 5f;
    bool isFacingRight = true;
    float jumpPower = 12f;
    bool isGrounded = false;
    bool isChargingJump = false;

    Animator animator;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == true && isChargingJump == false) { moveSpeed = 10f; }
        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded == true && isChargingJump == false)
        {
            moveSpeed = 5f;
        }


        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            animator.SetBool("JumpReady",isChargingJump = true);
            moveSpeed = 1f;
            

        }

        if (Input.GetButtonUp("Jump") && isGrounded && isChargingJump == true)
        {
            moveSpeed = 5f;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("JumpReady", isChargingJump = false);
            animator.SetBool("isJumping", !isGrounded);
        }
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            animator.SetTrigger("Attack1");
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity",Math.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity",rb.velocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true ;
        animator.SetBool("isJumping", !isGrounded);
    }



}
