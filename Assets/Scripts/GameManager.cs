
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject SeeEnemyTrigger;
    void Start()
    {
        Invoke("EnemyTriggerSetActiveTrue", 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyTriggerSetActiveTrue()
    {
        SeeEnemyTrigger.SetActive(true);
    }
}
