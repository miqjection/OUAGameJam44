using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public static PlayerAnimation instance;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private bool isIdle;
    public bool isWalk;
    [SerializeField] private bool isDeath;
    [SerializeField] private bool isAtack;

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
        playerAnimator.SetBool("isAtack", isAtack);
        playerAnimator.SetBool("isDeath", isDeath);

    }
}
