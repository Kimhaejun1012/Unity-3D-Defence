using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [Header("Option Panel")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Button koreanButton;
    public Button englishButton;

    void Start()
    {
        SoundManager.Instance.masterSlider = masterSlider;
        SoundManager.Instance.bgmSlider = bgmSlider;
        SoundManager.Instance.sfxSlider = sfxSlider;

        masterSlider.value = SaveManager.data.masterVolume;
        bgmSlider.value = SaveManager.data.bgmVolume;
        sfxSlider.value = SaveManager.data.sfxVolume;

        masterSlider.onValueChanged.AddListener(SoundManager.Instance.SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SoundManager.Instance.SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);

        koreanButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetLangauge("ko");
        });
        englishButton.onClick.AddListener(() =>
        {
            GameManager.Instance.SetLangauge("en");
        });
    }
}
