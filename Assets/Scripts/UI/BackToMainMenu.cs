using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    public int timerSecs = 3;
    private void OnEnable()
    {
        StartCoroutine(CountDouwn());
    }

    IEnumerator CountDouwn()
    {
        yield return new WaitForSeconds(timerSecs);
        SceneManager.LoadScene("MainMenuScene");
    }
}
