using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    AudioSource[] sesEfektleri;

    private void Awake()
    {
        Instance = this;
    }
    public void sesEfektiCikar(int hangiSes)
    {
        sesEfektleri[hangiSes].Stop();
        sesEfektleri[hangiSes].Play();
    }
}
