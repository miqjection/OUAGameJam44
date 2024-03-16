using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private bool isGrounded;

    private float horizontalInput;
    private float scaleX;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        scaleX = transform.localScale.x;
    }

    void Update()
    {
        WalkAnimation();
        LookRotation();

        print(isGrounded);


        if (isGrounded)
            Walk();


        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //isGrounded = false;
        }

        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0f)
        {
            //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void Walk()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput *100 * speed * Time.deltaTime, 0);

        rb.velocity = movement;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    private void WalkAnimation()
    {
        if (horizontalInput != 0)
        {
            PlayerAnimation.instance.isWalk = true;
        }

        else
        {
            PlayerAnimation.instance.isWalk = false;
        }
    }

    private void LookRotation()
    {
        if(horizontalInput < 0)
        {
            transform.localScale = new Vector2 (-scaleX, transform.localScale.y);
        }

        else if(horizontalInput > 0) 
        {
            transform.localScale = new Vector2(scaleX, transform.localScale.y); 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
