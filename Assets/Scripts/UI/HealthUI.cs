using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI _hpText;
    
    private void Start()
    {
        _hpText = GetComponent<TextMeshProUGUI>();
        GlobalHealth.instance.OnHealthChange += UpdateUI;
    }

    private void UpdateUI(object sender, int e)
    {
        _hpText.text = e.ToString();
    }
}
