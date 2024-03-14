using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderController : MonoBehaviour
{
    public static SpiderController instance; 

    [SerializeField]
    Transform[] pozisyonlar;

    [SerializeField]
    Slider orumcekSlider;

    public int maxSaglik;
    public int gecerliSaglik;

    public float spiderSpeed;
    public float beklemeSuresi;
    float beklemeSayac;

    Animator anim;

    int kacinciPos;

    Transform hedefPlayer;

    public float takipMesafesi = 5f;

    BoxCollider2D spiderCollider;

    Rigidbody2D rb;

    bool atakYapabilirMi;

    public float xBoyut, yBoyut;

    [SerializeField]
    GameObject iksirPrefab;

    public bool Bossmu;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spiderCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        instance = this;

    }

    private void Start()
    {
        gecerliSaglik = maxSaglik;
        orumcekSlider.maxValue = maxSaglik;
        SliderGuncelle();
        atakYapabilirMi = true;
        hedefPlayer = GameObject.Find("Player").transform;
        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;

        }
        
    }

    private void Update()
    {
        if (atakYapabilirMi == false)
        {
            return;
        }
        if (beklemeSayac > 0)
        {
            //örümcek verilen noktada duruyor
            beklemeSayac -= Time.deltaTime;
            anim.SetBool("hareketEtsinmi", false);
        }
        else
        {
            if (hedefPlayer.position.x > pozisyonlar[0].position.x && hedefPlayer.position.x < pozisyonlar[1].position.x)
            {
                Vector3 yeniPozisyon = hedefPlayer.position;
                yeniPozisyon.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, yeniPozisyon, spiderSpeed * Time.deltaTime);
                anim.SetBool("hareketEtsinmi", true);
                if (transform.position.x > hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(-xBoyut, yBoyut, 1);
                }
                else if (transform.position.x < hedefPlayer.position.x)
                {
                    transform.localScale = new Vector3(xBoyut, yBoyut, 1);

                }


            }
            else
            {
                anim.SetBool("hareketEtsinmi", true);

                if (transform.position.x > pozisyonlar[kacinciPos].position.x)
                {
                    transform.localScale = new Vector3(-xBoyut, yBoyut, 1);
                }
                else if (transform.position.x < pozisyonlar[kacinciPos].position.x)
                {
                    transform.localScale = new Vector3(xBoyut, yBoyut, 1);

                }

                transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPos].position, spiderSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, pozisyonlar[kacinciPos].position) < 0.1f)
                {
                    beklemeSayac = beklemeSuresi;
                    PozisyonuDegistir();
                }
            }


        }
    }
    void PozisyonuDegistir()
    {
        kacinciPos++;

        if (kacinciPos >= pozisyonlar.Length)
        {
            kacinciPos = 0;
        }
    }

    //Örümcegin takip mesafini ölcen kod
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;

        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (spiderCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirMi == true  && Bossmu==false)
        {
            atakYapabilirMi = false;
            anim.SetTrigger("atakYapti");
            other.GetComponent<PlayerHareketController>().GeriTepkiFNC();
            other.GetComponent<PlayerHealthController>().CaniAzalt();
            StartCoroutine(YenidenAttackYap());
        }

        else if (Bossmu==true && spiderCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirMi == true)
        {
            atakYapabilirMi = false;
            anim.SetTrigger("atakYapti");
            other.GetComponent<PlayerHareketController>().PlayerDied();
        }
    }

    IEnumerator YenidenAttackYap()
    {
        yield return new WaitForSeconds(1f);
        if (gecerliSaglik > 0)
            atakYapabilirMi = true;

    }

    public IEnumerator GeriTepkiFNC()
    {

        atakYapabilirMi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);

        gecerliSaglik--;
        SoundManager.Instance.sesEfektiCikar(9);
        SliderGuncelle();
        bossSliderGuncelle();

        if (gecerliSaglik <= 0)
        {
            atakYapabilirMi = false;
            gecerliSaglik = 0;
            
            int rand = Random.Range(0, 100);
            if (rand >= 80)
            {
                Instantiate(iksirPrefab, transform.position, Quaternion.identity);
            }
            anim.SetTrigger("canVerdi");
            SoundManager.Instance.sesEfektiCikar(6);
            rb.velocity = Vector2.zero;
            GetComponent<BoxCollider2D>().enabled = false;
            orumcekSlider.gameObject.SetActive(false);
            UIManager.instance.bossSliderKapat();

            Destroy(gameObject, 1.5f);

            
            
           
        }

       
        else
        {
           
                rb.velocity = new Vector2(-transform.localScale.x + 0.1f, rb.velocity.y);

                yield return new WaitForSeconds(0.05f);
           

            anim.SetBool("hareketEtsinmi", false);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            atakYapabilirMi = true;
        }

    }

    void SliderGuncelle()
    {
        orumcekSlider.value = gecerliSaglik;
    }

    void bossSliderGuncelle()
    {
        UIManager.instance.bosSlider(gecerliSaglik,maxSaglik);
    }
}
