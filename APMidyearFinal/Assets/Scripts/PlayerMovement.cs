using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float dirX = 0f;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 17f;
    [SerializeField] private LayerMask jumpableGround;
    private enum MovementState {idle, running, jumping, falling}
    private MovementState state = MovementState.idle;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    private void Update() //each frame
    {
        //throughout the whole         rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
         dirX = Input.GetAxisRaw("Horizontal");
         rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);

        }
        UpdateAnimationUpdate();
       
    }
   
    private void UpdateAnimationUpdate() 
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;

        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        

    }
}
