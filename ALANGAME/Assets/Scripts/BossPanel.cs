using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPanel : MonoBehaviour
{
    [SerializeField]
    GameObject bossPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossPanel.SetActive(true);
        }
    }

}
