using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;

    private Rigidbody2D rbBall;
    [SerializeField] private float ballForce;
    [SerializeField] private Transform dogHoldBallTransform;
    public bool canHoldBall = true;
    private bool isCollisionPlayer;
    private void Awake()
    {
        instance = this;
        rbBall = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        canHoldBall = true;
        Invoke("BallAdforce", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(isCollisionPlayer && canHoldBall)
            transform.position = dogHoldBallTransform.position;
    }

    private void BallAdforce()
    {
        rbBall.AddForce(Vector2.left * ballForce);
        rbBall.AddForce(Vector2.up * ballForce/1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canHoldBall)
        {
            isCollisionPlayer = true;
        }
    }
}
