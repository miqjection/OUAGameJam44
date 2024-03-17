
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject SeeEnemyTrigger;
    [SerializeField] private GameObject girlWithCatCar;
    [SerializeField] private GameObject girl;
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
        girl.SetActive(false);
        girlWithCatCar.SetActive(true);
    }
}
