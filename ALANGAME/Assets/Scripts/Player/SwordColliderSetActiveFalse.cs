using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderSetActiveFalse : MonoBehaviour
{
    public GameObject kilicCollider;

    public void KiliciKapat()
    {
        kilicCollider.SetActive(false);
    }
}
