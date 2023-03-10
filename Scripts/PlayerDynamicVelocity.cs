using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDynamicVelocity : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private float speed = 500;
    [SerializeField]
    private float jumpSpeed = 8;
    [SerializeField]
    private float maxSpeedVertical = 3;

    float horizontalInput = 0;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    bool isGrounded = false;

    //public Sounds playerSopunds;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //playerSopunds = GetComponentInChildren<Sounds>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        HandleMovementDirectionSpriteFlip();

        HandleGroundedCheck();

        HandleJumpInput();
    }

    private void HandleMovementDirectionSpriteFlip()
    {
        if (horizontalInput > 0)
            spriteRenderer.flipX = false;
        else if (horizontalInput < 0)
            spriteRenderer.flipX = true;
    }

    private void HandleGroundedCheck()
    {
        if (isGrounded)
        {
            //if (animator.GetBool("Jumping"))
            //    playerSopunds.PlayLandSound();
            animator.SetBool("Jumping", false);
            animator.SetFloat("InputX", Mathf.Abs(horizontalInput));
        }

        Debug.DrawRay(transform.position, Vector2.down * 0.5f);
        if (rb2d.velocity.y <= 0)
            isGrounded = IsGrounded();
    }

    private bool IsGrounded()
    {
        RaycastHit2D result = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        return result.collider != null;
    }

    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //playerSopunds.PlayJumpSound();
            animator.SetBool("Jumping", true);
            isGrounded = false;
            rb2d.AddRelativeForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = horizontalInput * this.speed * Time.fixedDeltaTime;
        float clampedSpeed = Mathf.Clamp(currentSpeed, -maxSpeedVertical, maxSpeedVertical);
        Vector2 velocity = new Vector2(clampedSpeed, rb2d.velocity.y);
        rb2d.velocity = velocity;
    }

}