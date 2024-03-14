using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class AnaMenuController : MonoBehaviour
{

    public static AnaMenuController Instance;

   


    private void Awake()
    {
        Instance = this;
    }
  
    public void OyunaBasla()
    {
        FadeController.instance.seffaftanMata();
        StartCoroutine(sahneyeGec());
    }

    IEnumerator sahneyeGec()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Giris_Sahnesi");
    }

    public void OyundanCik()
    {
        Application.Quit();
    }

  
    

}
