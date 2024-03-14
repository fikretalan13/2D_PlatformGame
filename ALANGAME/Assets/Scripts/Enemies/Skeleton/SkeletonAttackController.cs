using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackController : MonoBehaviour
{

    [SerializeField]
    Transform attackPos;

    [SerializeField]
    float atakYariCap;

    [SerializeField]
    LayerMask playerLayer;

    public void AttackYap()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(attackPos.position, atakYariCap, playerLayer);

        if (playerCollider != null && !playerCollider.GetComponent<PlayerHareketController>().playerDied)
        {
            playerCollider.GetComponent<PlayerHareketController>().GeriTepkiFNC();
            playerCollider.GetComponent<PlayerHealthController>().CaniAzalt();
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, atakYariCap);
    }
}
