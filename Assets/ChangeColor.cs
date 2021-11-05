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
      

        Debug.Log("Cambiando color");

        foreach (var VARIABLE in Materials)
        {
 
            VARIABLE.gameObject.LeanColor(newColor, time).setEase(inEase).setOnComplete(() =>
            {
                VARIABLE.gameObject.LeanColor(originalColor, time).setEase(outEase);
            });
        }
    }



}