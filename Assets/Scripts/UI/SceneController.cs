using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int round;
    public static SceneController _instance { get; private set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void GoToLoseScene()
    {
        PlayerPrefs.SetInt("round", round);
        SceneManager.LoadScene("LoseScreen");
        AudioManager.Instance?.Stop("Partida");
        AudioManager.Instance?.Stop("Finalizar Ronda");
        AudioManager.Instance?.Stop("Chill Theme");
        AudioManager.Instance?.Play("Perder Partida");
    }


    public void GoToWinScene()
    {
        SceneManager.LoadScene("WinScreen");
        AudioManager.Instance?.Stop("Partida");
        AudioManager.Instance?.Stop("Finalizar Ronda");
        AudioManager.Instance?.Stop("Chill Theme");
        AudioManager.Instance?.Play("Ganar Partida");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Economia");
    }
}