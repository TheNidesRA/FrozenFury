using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    private float fixedRotation;
    public RectTransform rct;
    private RectTransform canvas;


    private void Awake()
    {
        //canvas = gameObject.GetComponent<RectTransform>();
        canvas = GameObject.FindWithTag("MainCanvas").GetComponent<RectTransform>();
    }

    //https://forum.unity.com/threads/detecting-if-a-recttransform-is-outside-the-canvas.422186/
    void Update()
    {
        CheckIfInsideCanvas();
        fixedRotation = Mathf.Clamp(Camera.main.transform.eulerAngles.x, 0, 180);
        
        transform.eulerAngles = new Vector3(fixedRotation, Camera.main.transform.eulerAngles.y, 0);
    }


    private void CheckIfInsideCanvas()
    {
        Vector3[] corners = new Vector3[4];
        rct.GetWorldCorners(corners);
   
        
        
        foreach (var corner in corners)
        {
           // Debug.Log(corner);
            var localSpacePoint = canvas.InverseTransformPoint(corner);
           
            if (!canvas.rect.Contains(localSpacePoint))
            {
                Debug.Log("Esta fuera");
                
            }
        }

    }
    
    
}
