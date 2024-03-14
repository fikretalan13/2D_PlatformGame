using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHareketController : MonoBehaviour
{
    public static PlayerHareketController instance;

    Rigidbody2D rb;

    [SerializeField]
    Transform zeminKontrolNoktasi;

    [SerializeField]
    GameObject kilicCollider;
    
    Animator anim;

    public LayerMask zeminMask;

    public float hareketHizi;
    public float ziplamaGucu;

    bool zemindemi;

    [SerializeField]
    float geriTepkiSuresi, geriTepkiGucu;

    float geriTepkiSayaci;

    bool yonSagdami;

    [SerializeField]
    SpriteRenderer sr;

    public bool playerDied;

    bool attackYaptimi;

    int kacDefaBasti;

    [SerializeField] 
    float m_rollForce = 6.0f;

    private int m_facingDirection = 1;
    float beklemeSuresi = 3f;
    private float m_rollCurrentTime;
    private bool m_rolling = false;


    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        kilicCollider.SetActive(false);

        playerDied = false;
    }

    private void Update()
    {
        
        if (m_rollCurrentTime > beklemeSuresi)
            m_rolling = false;
       

        if (playerDied)
            return;


        if (Input.GetKeyDown(KeyCode.F) && !m_rolling)
        {
            kacDefaBasti++;
            attackYaptimi = true;
            kilicCollider.SetActive(true);
            SoundManager.Instance.sesEfektiCikar(4);
            if (kacDefaBasti == 1)
            {
                if (attackYaptimi)
                    anim.SetTrigger("attack_1");
            }

            else if (kacDefaBasti == 2)
            {

                if (attackYaptimi)
                    anim.SetTrigger("attack_2");
            }


            else if (kacDefaBasti == 3)
            {

                if (attackYaptimi)
                    anim.SetTrigger("attack_3");

                kacDefaBasti = 0;
            }

          


        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !m_rolling)
        {
            m_rollCurrentTime = beklemeSuresi;
           // m_rolling = true;
            anim.SetTrigger("Roll");
            rb.velocity = new Vector2(m_facingDirection * m_rollForce, rb.velocity.y);
            m_rollCurrentTime -= Time.deltaTime;
            
        }

        else
            attackYaptimi = false;

        if (geriTepkiSayaci <= 0)
        {
            HareketEt();
            ZiplaFNC();
            YonuDegistir();


            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
        }



      





        else
        {
            geriTepkiSayaci -= Time.deltaTime;
            if (yonSagdami)
            {
                rb.velocity = new Vector2(-geriTepkiGucu,rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(geriTepkiGucu, rb.velocity.y);
            }

        }
     
       

        anim.SetBool("zemindemi", zemindemi);
        anim.SetFloat("hareketHizi",Mathf.Abs(rb.velocity.x));
       
      

    }


    //Playeri hareket ettirmek için gereken kodlar.
    void HareketEt()
    {
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * hareketHizi, rb.velocity.y);
       
               
    }


    void YonuDegistir()
    {
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-2.2079f, 2.2079f, 1);
            yonSagdami = false;
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(2.2079f, 2.2079f, 1);
            yonSagdami=true;
        }

    }

    void ZiplaFNC()
    {
        zemindemi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, 0.5f, zeminMask);

        if (Input.GetButtonDown("Jump") && zemindemi)
        {
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);
            SoundManager.Instance.sesEfektiCikar(3);
        }

       
    }

    public void GeriTepkiFNC()
    {
        geriTepkiSayaci = geriTepkiSuresi;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        rb.velocity = new Vector2(0,rb.velocity.y);
    }

    public void PlayerDied()
    {
        rb.velocity = Vector2.zero;
        playerDied = true;
        
        UIManager.instance.SliderUpdate(0, 0);
        anim.SetTrigger("died");
        SoundManager.Instance.sesEfektiCikar(2);

        StartCoroutine(SahneyiYenidenYukle());

    }
    IEnumerator SahneyiYenidenYukle()
    {
        yield return new WaitForSeconds(2);

        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(2);

        //var olan sahneyi yeniden yükler
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void PlayeriHareketsizYap()
    {
        rb.velocity = Vector2.zero;
        anim.SetFloat("hareketHizi",0f);
    }

}
