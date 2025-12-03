using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(this);

        SaveManager.Load();
        Localization.LoadLanguage(SaveManager.data.language);
    }
    public void GameOver()
    {
        Debug.Log("Game Over~!");
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
    }
    public void SetLangauge(string lang)
    {
        Localization.SetLanguage(lang);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
