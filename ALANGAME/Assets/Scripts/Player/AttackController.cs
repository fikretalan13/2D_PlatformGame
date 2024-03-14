using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D swordCollider;

    [SerializeField]
    GameObject parlamaEfekti;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (swordCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (other.CompareTag("Spider"))
            {




                StartCoroutine(other.GetComponent<SpiderController>().GeriTepkiFNC());
                Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
            }
        }


        if (swordCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (other.CompareTag("Bat"))
            {

                StartCoroutine(other.GetComponent<BatController>().GeriTepkiFNC());
                Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
            }
        }

        if (swordCollider.IsTouchingLayers(LayerMask.GetMask("SkeletonLayer")))
        {
            if (other.CompareTag("Skeleton"))
            {

                StartCoroutine(other.GetComponent<SkeletonHealthController>().CaniAzaltFnc());
                SkeletonHealthController.Instance.SliderGuncelle();

                Instantiate(parlamaEfekti, other.transform.position, Quaternion.identity);
                
               
            }
        }
    }
}
