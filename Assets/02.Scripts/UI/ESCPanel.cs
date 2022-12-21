using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ESCPanel : MonoBehaviour
{
    [SerializeField]
    private Button continueBtn;
    [SerializeField]
    private Button settingBtn;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private GameObject settingPanel;
    [HideInInspector]
    public bool isOpen;

    [SerializeField]
    private AudioMixer masterMixer;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider soundEffectSlider;

    [SerializeField]
    private Button closeBtn;
    public void Awake()
    {
        exitBtn.onClick.AddListener(Exit);
        settingBtn.onClick.AddListener(SettingPanel);
        continueBtn.onClick.AddListener(Continue);
        closeBtn.onClick.AddListener(OnCloseSetting);
    }

    public void OnPanel()
    {
        isOpen = true;
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void Continue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
    private void SettingPanel()
    {
        settingPanel.SetActive(true);
    }
    private void Exit()
    {
        GameManager.Inst.Exit();
    }

    public void OffPanel()
    {
        isOpen = false;
        settingPanel.SetActive(false);
        gameObject.SetActive(false);
    }
    
    public void BGMSoundSet(float sliderValue)
    {
        masterMixer.SetFloat("BGMParam", Mathf.Log10(sliderValue) * 20);
    }

    public void SoundEffectSet(float sliderValue)
    {
        masterMixer.SetFloat("SoundEffectParam", Mathf.Log10(sliderValue) * 20);
    }
    private void OnCloseSetting()
    {
        settingPanel.SetActive(false);
    }
}
