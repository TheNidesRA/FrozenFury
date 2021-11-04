using System.Collections;
using System.Collections.Generic;
using Enemies;
using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    private TextMeshProUGUI roundText;
    // Start is called before the first frame update
    void Start()
    {
        roundText = GetComponent<TextMeshProUGUI>();
        WaveController._instance.OnRoundChange += UpdateRoundTxt;
    }

    private void UpdateRoundTxt(object sender, int e)
    {
        roundText.text = "Round: " + e.ToString();
    }
}
