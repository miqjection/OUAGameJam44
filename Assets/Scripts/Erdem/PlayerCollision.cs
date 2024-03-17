using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SeeEnemyTrigger"))
        {
            BallManager.instance.canHoldBall = false;
            PlayerController.instance.canMove = false;

            print("KIZ KAÇIRILIYORRRRR");
        }
    }
}
