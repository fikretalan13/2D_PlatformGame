using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulture : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    public float vultureSpeed;
    public float beklemeSuresi;

    float beklemeSayac;

    Animator anim;

    int kacinciPozisyon;

    private void Awake()
    {

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }
        
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        

        kacinciPozisyon = 0;
        transform.position = pozisyonlar[kacinciPozisyon].position;
    }

    private void Update()
    {

        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            
        }

        if (transform.position.x > pozisyonlar[kacinciPozisyon].position.x)
        {
            transform.localScale = new Vector3(-5.614f, 5.614f, 5.614f);
        }
        else
        {
            transform.localScale = new Vector3(5.614f, 5.614f, 5.614f);
        }

        transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, vultureSpeed * Time.deltaTime);
       

        if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
        {

            PozisyonuDegistir();
            beklemeSayac = beklemeSuresi;

        }
    }

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;
        if (kacinciPozisyon >= pozisyonlar.Length)
            kacinciPozisyon = 0;
    }
}
