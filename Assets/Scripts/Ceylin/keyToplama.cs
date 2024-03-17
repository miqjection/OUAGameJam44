using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyToplama : MonoBehaviour
{
    public int playerKey;
   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Key")
        {
            playerKey++;
            Destroy(other.gameObject);
        }
    }
}
