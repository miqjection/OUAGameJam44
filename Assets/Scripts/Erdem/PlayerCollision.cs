using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public static PlayerCollision Instance;

    public bool button1Collision;
    public bool button2Collision;
    public bool button3Collision;

    private void Awake()
    {
        Instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SeeEnemyTrigger"))
        {
            BallManager.instance.canHoldBall = false;
            PlayerController.instance.canMove = false;

            InvokeRepeating("Escape", 2, 0.003f);
        }

        { 
            if (collision.gameObject.CompareTag("Button1"))
            {
                button1Collision = true;

                button2Collision = false;
                button3Collision = false;
            }

            if (collision.gameObject.CompareTag("Button2"))
            {
                button2Collision = true;

                button1Collision = false;
                button3Collision = false;
            }
            if (collision.gameObject.CompareTag("Button3"))
            {
                button3Collision = true;

                button1Collision = false;
                button2Collision = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            print("ad");
            PlayerController.instance.canMove = false;
            PlayerAnimation.instance.isDead = true;
        }
    }

    private void Escape()
    {
        CatEscape.instance.CatEscaping();
    }
}

