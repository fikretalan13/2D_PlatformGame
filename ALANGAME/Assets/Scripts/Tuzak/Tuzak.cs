using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuzak : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHareketController.instance.PlayerDied();
            
        }
    }

   
}
