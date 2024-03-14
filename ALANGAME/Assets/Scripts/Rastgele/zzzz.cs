using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zzzz : MonoBehaviour
{
    private void Update()
    {
        if (SpiderController.instance.gecerliSaglik==0)
        {
            
            StartCoroutine(birazBekle());
        }
    }

    IEnumerator birazBekle()
    {
        yield return new WaitForSeconds(4);
        
        GameManager.Instance.OyunBittiPaneli();

    }
}
