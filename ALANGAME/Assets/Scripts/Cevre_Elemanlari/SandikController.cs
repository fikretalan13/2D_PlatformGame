using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandikController : MonoBehaviour
{
    [SerializeField]
    GameObject parlamaEfekti;

    Animator anim;

    int kacinciVurus;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("kilicCollider"))
        {
            if (kacinciVurus==0)
            {
                anim.SetTrigger("sallanma");
                Instantiate(parlamaEfekti,transform.position,transform.rotation);
            }
            else if (kacinciVurus == 1)
            {
                anim.SetTrigger("sallanma");
                Instantiate(parlamaEfekti, transform.position, transform.rotation);
            }
            else 
            {
                anim.SetTrigger("parcalanma");
                SoundManager.Instance.sesEfektiCikar(7);

                Destroy(gameObject,2f);
            }
            kacinciVurus++;

        }
    }
}
