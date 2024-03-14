using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LeverController : MonoBehaviour
{

   

    Animator anim;


    [SerializeField]
    GameObject acilacakEngel;

    private void Awake()
    {
        anim = GetComponent<Animator>();
   
    }

   

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("kilicCollider"))
        {

            anim.SetBool("acilsinmi", true);

            acilacakEngel.transform.DOLocalMoveY(acilacakEngel.transform.localPosition.y + 0.4f, 2.5f);
        }
    }







}
