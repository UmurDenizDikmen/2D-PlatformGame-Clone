using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rg;
    private BoxCollider2D coll;
    private Animator animator;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private LayerMask jumpableGround;
    public AudioSource jumpVoice;
   
    private enum MovementState {idle,running,jumping,falling }
  
    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        MovementState state;
        float horizontalMove = Input.GetAxisRaw("Horizontal");
            
        rg.velocity = new Vector2(horizontalMove * moveSpeed,rg.velocity.y);

        if (Input.GetButtonDown("Jump")&& IsGrounded())
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpSpeed);
            jumpVoice.Play();
        }
        if(horizontalMove != 0)
        {
            state = MovementState.running;
            SetFaceDirection();
        }
        else
        {
            state = MovementState.idle;
        }
        if (rg.velocity.y > .1f)
        {
            state = MovementState.jumping;
         

        }
        else if (rg.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        animator.SetInteger("state", (int)state);
    }
    
    public bool IsFaceDirection = true;
    public bool isFacingRight
    {
        get
        {
            return IsFaceDirection;
        }
        private set
        {
            if (IsFaceDirection != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            IsFaceDirection = value;
        }
    }
    public void SetFaceDirection()
    {
        if(rg.velocity.x >0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if(rg.velocity.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }
    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f,Vector2.down,.1f, jumpableGround);
    }
}
  
