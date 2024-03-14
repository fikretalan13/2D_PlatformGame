using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxSaglik, gecerliSaglik;

    Animator anim;


    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {

        gecerliSaglik = maxSaglik;

        if (UIManager.instance!=null)
        {
            UIManager.instance.SliderUpdate(gecerliSaglik, maxSaglik);
        }
        
    }

    public void CaniAzalt()
    {
        gecerliSaglik--;
        UIManager.instance.SliderUpdate(gecerliSaglik, maxSaglik);
        anim.SetTrigger("caniYandi");
        SoundManager.Instance.sesEfektiCikar(1);

        if (gecerliSaglik<=0)
        {
            gecerliSaglik=0;
            PlayerHareketController.instance.PlayerDied();
            SoundManager.Instance.sesEfektiCikar(2);
            
        }
    }

    public void CaniArttir()
    {
      gecerliSaglik=maxSaglik;

        UIManager.instance.SliderUpdate(gecerliSaglik,maxSaglik);
    }

   
}
