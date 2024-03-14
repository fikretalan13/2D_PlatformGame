using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goNext : MonoBehaviour
{
    public string digerSahne;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FadeController.instance.seffaftanMata();
            other.GetComponent<PlayerHareketController>().PlayeriHareketsizYap();
            other.GetComponent<PlayerHareketController>().enabled = false;
            StartCoroutine(digerSahneyeGec());   
        }
    }
    IEnumerator digerSahneyeGec()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(digerSahne);
    }
}
