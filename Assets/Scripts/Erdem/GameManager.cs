
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject SeeEnemyTrigger;
    [SerializeField] private GameObject girlWithCatCar;
    [SerializeField] private GameObject girl;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Invoke("EnemyTriggerSetActiveTrue", 5);
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
