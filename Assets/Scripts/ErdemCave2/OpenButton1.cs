using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton1 : MonoBehaviour
{
    [SerializeField] private GameObject openButton1;
    [SerializeField] private GameObject openButton2;
    [SerializeField] private GameObject openButton3;

    [SerializeField] private GameObject closeButton1;
    [SerializeField] private GameObject closeButton2;
    [SerializeField] private GameObject closeButton3;

    [SerializeField] private GameObject door1;
    [SerializeField] private GameObject door2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (PlayerCollision.Instance.button1Collision)
            {
                closeButton1.SetActive(false);
                openButton1.SetActive(true);

                InvokeRepeating("OpenDoor", 0.3f, 0.1f);
            }

            else if (PlayerCollision.Instance.button2Collision)
            {
                closeButton2.SetActive(false);
                openButton2.SetActive(true);
            }

            else if (PlayerCollision.Instance.button3Collision)
            {
                closeButton3.SetActive(false);
                openButton3.SetActive(true);
            }
        }
    }

    private void OpenDoor()
    {
        door1.transform.position += new Vector3(0, 0.01f, 0);
        door2.transform.position += new Vector3(0, -0.01f, 0);
    }
}
