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
    }


    public void GoToWinScene()
    {
        SceneManager.LoadScene("WinScreen");
    }
}
