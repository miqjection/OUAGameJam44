using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private bool isIdle;
    public bool isWalk;
    public bool isDead;
    [SerializeField] private bool isAttack;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        playerAnimator.SetBool("isWalk", isWalk);
        playerAnimator.SetBool("isAttack", isAttack);
        playerAnimator.SetBool("isDead", isDead);

    }
}
