using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Image loadingImg;
    public TextMeshProUGUI text;

    public int sceneID;

    public void Start()
    {
        StartCoroutine(nameof(AsyncLoad));
    }

    IEnumerator AsyncLoad()
    {
        var operation = SceneManager.LoadSceneAsync(sceneID);
        while (!operation.isDone)
        {
            var progress = operation.progress / 0.9f;
            loadingImg.fillAmount = progress;
            text.text = $"{progress * 100:0}%";
            yield return null;
        }
    }
}