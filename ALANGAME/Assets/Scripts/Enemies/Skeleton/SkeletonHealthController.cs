using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkeletonHealthController : MonoBehaviour
{
    public static SkeletonHealthController Instance;

    public int maxSaglik;
   public int gecerliSaglik;

    public bool iskeletOldumu;

    [SerializeField]
    Slider SkeletonSlider;

    Animator anim;

    Rigidbody2D rb;

    [SerializeField]
    GameObject iksirPrefab;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        iskeletOldumu = false;
        Instance = this;
        gecerliSaglik = maxSaglik;
        SkeletonSlider.maxValue = maxSaglik;

        SliderGuncelle();
        
    }

    public IEnumerator CaniAzaltFnc()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);
        gecerliSaglik--;
        SoundManager.Instance.sesEfektiCikar(9);

        SliderGuncelle();

        if (gecerliSaglik <= 0)
        {
            iskeletOldumu = true;
            gecerliSaglik = 0;
           
            anim.SetTrigger("canVerdi");
            SoundManager.Instance.sesEfektiCikar(10);
            int rand = Random.Range(0, 100);
            if (rand >= 80)
            {
                Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            }
            GetComponent<BoxCollider2D>().enabled = false;
            SkeletonSlider.gameObject.SetActive(false);
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 1.5f);


        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);

                yield return new WaitForSeconds(0.05f);
            }

          
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            
        }

    }

    public void SliderGuncelle()
    {
        SkeletonSlider.value = gecerliSaglik;
    }


}
