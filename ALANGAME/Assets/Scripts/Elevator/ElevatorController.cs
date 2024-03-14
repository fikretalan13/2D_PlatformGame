using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField]
    Transform[] pozisyonlar;

    

    public float elevatorSpeed;
    public float beklemeSuresi;

    float beklemeSayac;

    int kacinciPozisyon;

    private void Awake()
    {

        foreach (Transform pos in pozisyonlar)
        {
            pos.parent = null;
        }

        
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

       

        transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacinciPozisyon].position, elevatorSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, pozisyonlar[kacinciPozisyon].position) < 0.1f)
        {
            beklemeSayac = beklemeSuresi;
            PozisyonuDegistir();
           

        }
    }

    void PozisyonuDegistir()
    {
        kacinciPozisyon++;
        if (kacinciPozisyon >= pozisyonlar.Length)
            kacinciPozisyon = 0;
    }

}
