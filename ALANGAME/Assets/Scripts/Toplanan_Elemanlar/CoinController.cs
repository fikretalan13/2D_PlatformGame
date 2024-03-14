using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    bool coinmi;

    bool toplandimi;

    [SerializeField]
    GameObject coinEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !toplandimi)
        {
            toplandimi = true;
            GameManager.Instance.toplananCoinAdet += 10;
            SoundManager.Instance.sesEfektiCikar(5);
            UIManager.instance.CoinTextUpdate();
            Instantiate(coinEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
