using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
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
    private int sfxIndex = 0;

    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

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
        masterVolume = value;
        mixer.SetFloat("Master", LinearToDecibel(value));
    }

    public void SetBGMVolume(float value)
    {
        bgmVolume = value;
        mixer.SetFloat("BGM", LinearToDecibel(value));
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        mixer.SetFloat("SFX", LinearToDecibel(value));
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
        SetMasterVolume(masterVolume);
        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    private float LinearToDecibel(float linear)
    {
        if (linear <= 0.0001f)
            return -80f;

        return Mathf.Log10(linear) * 20f;
    }
}
