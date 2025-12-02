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
    }
    public void GameOver()
    {
        Debug.Log("Game Over~!");
    }
    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        UIManager.Instance.ShowPausePanel();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        UIManager.Instance.HidePausePanel();
    }
    public void ExitToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public void LoadLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void SetGameSpeed(float speed)
    {
        Time.timeScale = speed;
        UIManager.Instance.HidePausePanel();
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
