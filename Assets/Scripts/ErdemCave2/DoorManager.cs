using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private GameObject openDoor;
    [SerializeField] private GameObject closeDoor;

    public bool hasKey;

    public static DoorManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasKey)
        {
            closeDoor.SetActive(false);
            openDoor.SetActive(true);
        }
    }
}
