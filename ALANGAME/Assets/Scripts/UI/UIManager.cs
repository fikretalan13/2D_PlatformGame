using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField]
    Slider playerSlider;

    [SerializeField]
    Slider bossSlider;


    [SerializeField]
    TMP_Text coinText;

    [SerializeField]
    GameObject pausePanel;

    [SerializeField]
    TMP_Text bossNameText;


    [SerializeField]
    GameObject bitisPanel;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        bitisPanel.SetActive(false);
    }


    public void SliderUpdate(int gecerliDeger, int maxDeger)
    {
        playerSlider.maxValue = maxDeger;
        playerSlider.value = gecerliDeger;
    }

    public void CoinTextUpdate()
    {
        coinText.text = GameManager.Instance.toplananCoinAdet.ToString();
    }

    
    public void bosSlider(int gecerliDeger, int maxDeger)
    {
        bossSlider.maxValue = maxDeger;
        bossSlider.value = gecerliDeger;
    }

    public void PausePanelAcKapat()
    {
        if (!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void AnaMenuyeDon()
    {
        FadeController.instance.seffaftanMata();
        SceneManager.LoadScene("AnaMenu");
        
       
        
    }

    public void TekrarOyna()
    {
        FadeController.instance.seffaftanMata();
        SceneManager.LoadScene("Giris_Sahnesi");
       
    }

    public void bossSliderKapat()
    {
        bossSlider.enabled = false;
        bossNameText.enabled = false;
    }

    public void BitisPaneliniAc()
    {
        bitisPanel.SetActive(true);
        SoundManager.Instance.sesEfektiCikar(11);
        Time.timeScale = 0;
    }

    public void OyundanCik()
    {
        Application.Quit();
    }
}
