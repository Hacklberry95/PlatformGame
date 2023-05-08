using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D collider;

    private float AxisX = 0f;
    private float movementSpeed = 14f;
    private float jumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpAudioSource;

    private enum MovementType { idle, running, jumping, falling }
    
    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
    }
        
    // Update is called once per frame
    private void Update()
    {
        AxisX = Input.GetAxisRaw("Horizontal");

        rigidBody.velocity = new Vector2(AxisX * jumpForce, rigidBody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            jumpAudioSource.Play();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, movementSpeed);
        }

        UpdateAnimationState();


    }
    private void UpdateAnimationState()
    {
        MovementType type;

        if (AxisX > 0f)
        {
            type = MovementType.running;
            spriteRenderer.flipX = false;
        }
        else if (AxisX < 0f)
        {
            type = MovementType.running;
            spriteRenderer.flipX = true;
        }
        else
        {
            type = MovementType.idle;
        }

        //.1f because the value is never truely 0
        if(rigidBody.velocity.y > .1f)
        {
            type = MovementType.jumping;
        }
        else if(rigidBody.velocity.y < -.1f)
        {
            type = MovementType.falling;
        }

        animator.SetInteger("state", (int)type);
    }

    private bool IsOnGround()
    {
       return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
