using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    private TextMeshProUGUI _goldText;
    
    // Start is called before the first frame update
    void Start()
    {
        _goldText = GetComponent<TextMeshProUGUI>();
        PlayerStats._instance.OnGoldChanged += UdateUI;
        _goldText.text = PlayerStats._instance.gold.ToString();
    }

    private void UdateUI(object sender, float e)
    {
        _goldText.text = e.ToString();
    }

}
