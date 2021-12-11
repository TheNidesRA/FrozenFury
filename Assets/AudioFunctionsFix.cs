using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFunctionsFix : MonoBehaviour
{
    public AudioAuxFunctions aux;

    // Start is called before the first frame update
    private void Awake()
    {
        aux.StopChillMusic();
    }

    // Update is called once per frame
    void Update()
    {
    }
}