using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 8f;
    private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource jumpSound;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCol;
    private float xDirection = 0f;
    private enum MovementState { idle, running, jumping, falling }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 2;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(rb.bodyType != RigidbodyType2D.Static)
        {
            xDirection = Input.GetAxisRaw("Horizontal"); //GetAxisRaw - immediate value change
            rb.velocity = new Vector2(moveSpeed * xDirection, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && IsGrounded() == true) //Using input system values
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            UpdateAnimatorState();
        }
    }

    private void UpdateAnimatorState()
    {
        MovementState state;
        if (xDirection > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (xDirection < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
            //animator.SetBool("isRunning", false); - for simple animation state
        }
        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCol.bounds.center, boxCol.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }
}
