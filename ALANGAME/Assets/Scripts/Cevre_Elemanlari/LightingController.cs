using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingController : MonoBehaviour
{


    [SerializeField]
    SpriteRenderer sr;

    [SerializeField]
    Sprite LightOn, LightOff;


    private void Awake()
    {
        sr.sprite = LightOff;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sr.sprite = LightOn;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            sr.sprite = LightOff;
        }
    }
}
