using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    [SerializeField]
    bool potionmu;

    bool toplandimi;

    [SerializeField]
    GameObject iksirEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !toplandimi)
        {
            toplandimi = true;
            Instantiate(iksirEffect,transform.position,Quaternion.identity);
            SoundManager.Instance.sesEfektiCikar(8);
            PlayerHealthController.instance.CaniArttir();
            UIManager.instance.CoinTextUpdate();
           
            Destroy(gameObject);
        }
    }
}
