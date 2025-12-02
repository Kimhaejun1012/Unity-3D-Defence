using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Mixer")]
    public AudioMixer mixer;

    [Header("Sources")]
    public AudioSource bgmSource;
    public AudioSource[] sfxSources;
    public SFXData sfxDB;
    public BGMData bgmDB;

    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;

    private int sfxIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        ApplyVolumes();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void PlayBGM(AudioClip clip)
    {
        if (clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource src = sfxSources[sfxIndex];
        src.PlayOneShot(clip);
        sfxIndex = (sfxIndex + 1) % sfxSources.Length;
    }
    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("Master", LinearToDecibel(value));
        SaveManager.data.masterVolume = value;
    }

    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGM", LinearToDecibel(value));
        SaveManager.data.bgmVolume = value;
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", LinearToDecibel(value));
        SaveManager.data.sfxVolume = value;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Lobby":
                PlayBGM(bgmDB.lobby);
                break;

            case "Game":
                PlayBGM(bgmDB.game);
                break;
        }
    }
    private void ApplyVolumes()
    {
        SetMasterVolume(SaveManager.data.masterVolume);
        SetBGMVolume(SaveManager.data.bgmVolume);
        SetSFXVolume(SaveManager.data.sfxVolume);

        masterSlider.value = SaveManager.data.masterVolume;
        bgmSlider.value = SaveManager.data.bgmVolume;
        sfxSlider.value = SaveManager.data.sfxVolume;
    }

    private float LinearToDecibel(float linear)
    {
        if (linear <= 0.0001f)
            return -80f;

        return Mathf.Log10(linear) * 20f;
    }
}
