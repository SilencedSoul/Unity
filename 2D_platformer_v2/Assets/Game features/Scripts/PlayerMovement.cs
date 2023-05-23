using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpHeight = 14f;
    bool doubleJump = false;

    // For sound effects
    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState { idle, running, jumping, falling, doublejump };

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        // Checking if the player have an animation (the animator component)
        if (gameObject.GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // This gives joystick support, as you can adjust the input (how much you push the joystick)
        // and the speed will also vary accordingly
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (IsGrounded())
        {

            doubleJump = false;
        }

        /* When jumping from the edge: 
         * Pressing the jump button once will register as two doublejumps (doubleJump = true is set twice)
         * The running animation will be played instead 
         * 
         * Fixed by setting a transition from running to double jump directly
         */

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }

            else if (!IsGrounded() && !doubleJump)
            {
                doubleJump = true;
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
        }

        if (gameObject.GetComponent<Animator>() != null)
        {
            UpdateAnimationState();
        }
          
    }

    private void UpdateAnimationState()
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

        /* if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        } */

        if (rb.velocity.y > 0.1f && doubleJump == false)
        {
            state = MovementState.jumping;
        }

        else if (rb.velocity.y > 0.1f && doubleJump == true)
        {
            state = MovementState.doublejump;
        }

        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);     
    }

    private bool IsGrounded()
    {
        // This makes the box smaller so that it will not collide with walls and allow the player to jump multiple times
        Vector2 smallbox = new Vector2(coll.bounds.size.x - 0.1f, coll.bounds.size.y);
        return Physics2D.BoxCast(coll.bounds.center, smallbox, 0f, Vector2.down, 0.1f, jumpableGround);

        /* This checks if there is any ground below the character
         * However, this does not check if the edge of the box is colliding with the ground
         * return Physics2D.Raycast(bottom, Vector2.down, 0.1f, jumpableGround);
         */
    }
}
