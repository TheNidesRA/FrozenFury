using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    public List<MeshRenderer> Materials;
    private Color originalColor;
    public Color newColor;
    public float time;
    public LeanTweenType inEase;
    public LeanTweenType outEase;


    void Reset()
    {
        newColor = new Color(255, 0, 0, 140);
        time = 0.6f;
        inEase = LeanTweenType.easeInSine;
        outEase = LeanTweenType.easeOutSine;
    }


    private void Awake()
    {
        originalColor = Materials[0].material.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Cambiar()
    {
        if (Materials.Count > 0)
        {
            Debug.Log("Cambiando color");

            foreach (var VARIABLE in Materials)
            {
                if (VARIABLE.gameObject.activeSelf)
                {
                    VARIABLE.gameObject.LeanColor(newColor, time).setEase(inEase).setOnComplete(() =>
                    {
                        VARIABLE.gameObject.LeanColor(originalColor, time).setEase(outEase);
                    });
                }
               
            }
        }
    }
}