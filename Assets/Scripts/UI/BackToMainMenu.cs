using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;



public class BackToMainMenu : MonoBehaviour
{
    // public int timerSecs = 3;
    // private void OnEnable()
    // {
    //     StartCoroutine(CountDouwn());
    // }
    //
    // IEnumerator CountDouwn()
    // {
    //     yield return new WaitForSeconds(timerSecs);
    //     SceneManager.LoadScene("MainMenuScene");
    //     AudioManager.Instance?.Stop("Partida");
    //     AudioManager.Instance?.Play("Menu Principal");
    // }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
        AudioManager.Instance?.Stop("Partida");
        AudioManager.Instance?.Play("Menu Principal");
    }


    public void PlayAgain()
    {
        SceneManager.LoadScene("Economia");
    }
    
    
    
}
