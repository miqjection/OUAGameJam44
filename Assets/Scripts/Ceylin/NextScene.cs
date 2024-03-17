using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour
{
    [SerializeField] private int SceneIndex;
    private Scene _scene;
    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();

    }
    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.tag=="Untagged")
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }
    }
