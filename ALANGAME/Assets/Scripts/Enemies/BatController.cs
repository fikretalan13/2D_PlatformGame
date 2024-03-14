using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatController : MonoBehaviour
{
    public static BatController instance;
    [SerializeField]
    float takipMesafesi = 8f;

    [SerializeField]
    float ucusHizi;

    [SerializeField]
    Transform hedefPlayer;

    [SerializeField]
    Slider batSlider;

    Animator anim;

    Rigidbody2D rb;

    BoxCollider2D batCollider;

    public float atakYapmaSuresi;
    float atakYapmaSayaci;

    Vector2 hareketYonu;

    float mesafe;

    public int maxSaglik;
    int gecerliSaglik;

    [SerializeField]
    GameObject iksirPrefab;

    bool atakYapabilirMi;
    private void Awake()
    {
        instance = this;
        hedefPlayer = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        batCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        atakYapabilirMi = true;
        gecerliSaglik = maxSaglik;
        batSlider.maxValue = maxSaglik;
        SliderGuncelle();
    }
    private void Update()
    {
        if (atakYapmaSayaci < 0)
        {
            if (hedefPlayer && gecerliSaglik > 0 && PlayerHareketController.instance.playerDied == false)
            {
                mesafe = Vector2.Distance(transform.position, hedefPlayer.position);
                if (mesafe < takipMesafesi)
                {
                    anim.SetTrigger("hareketEtsinmi");
                    hareketYonu = hedefPlayer.position - transform.position;

                    if (transform.position.x > hedefPlayer.position.x)
                    {
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else if (transform.position.x < hedefPlayer.position.x)
                    {
                        transform.localScale = Vector3.one;
                    }

                    rb.velocity = hareketYonu * ucusHizi;
                }
            }

        }
        else
        {
            atakYapmaSayaci -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (batCollider.IsTouchingLayers(LayerMask.GetMask("PlayerLayer")) && atakYapabilirMi == true)
        {
            if (other.CompareTag("Player"))
            {
                rb.velocity = Vector2.zero;
                atakYapmaSayaci = atakYapmaSuresi;
                anim.SetTrigger("atakYapti");

                other.GetComponent<PlayerHareketController>().GeriTepkiFNC();
                other.GetComponent<PlayerHealthController>().CaniAzalt();
                StartCoroutine(YenidenAttackYap());
            }
        }

    }

    public IEnumerator GeriTepkiFNC()
    {

        atakYapabilirMi = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(.1f);

        gecerliSaglik--;
        SliderGuncelle();
        SoundManager.Instance.sesEfektiCikar(9);

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
            SoundManager.Instance.sesEfektiCikar(0);

            GetComponent<BoxCollider2D>().enabled = false;
            batSlider.gameObject.SetActive(false);
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

            anim.SetBool("hareketEtsinmi", false);
            yield return new WaitForSeconds(0.25f);
            rb.velocity = Vector2.zero;
            atakYapabilirMi = true;
        }

    }

    IEnumerator YenidenAttackYap()
    {
        yield return new WaitForSeconds(1f);
        if (gecerliSaglik > 0)
            atakYapabilirMi = true;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, takipMesafesi);
    }

    void SliderGuncelle()
    {
        batSlider.value = gecerliSaglik;
    }
}
