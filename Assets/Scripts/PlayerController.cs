using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Walk();
        WalkAnimation();


    }

    private void Walk()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput *100 * speed * Time.deltaTime, 0);

        print(movement);
        rb.velocity = movement;
    }

    private void WalkAnimation()
    {
        if (horizontalInput != 0)
        {
            PlayerAnimation.instance.isWalk = true;
        }

        else
            PlayerAnimation.instance.isWalk = false;
    }

    private void LookRotation()
    {
        if(horizontalInput < 0)
        {
            
        }
    }
}
