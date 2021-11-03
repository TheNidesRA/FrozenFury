using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    private float fixedRotation;
    public RectTransform rct;
    private RectTransform canvas;
    private Camera _camera;
    public Vector3 _originalPosition;
    public GameObject pivot;
    public bool check = false;
    private void Start()
    {
        _originalPosition = rct.gameObject.transform.position;
        _camera = Camera.main;
        //canvas = gameObject.GetComponent<RectTransform>();
  
    }

    //https://forum.unity.com/threads/detecting-if-a-recttransform-is-outside-the-canvas.422186/
    void Update()
    {
    
        //CheckIfInsideCanvas();
        fixedRotation = Mathf.Clamp(Camera.main.transform.eulerAngles.x, 0, 180);
        
        transform.eulerAngles = new Vector3(fixedRotation, Camera.main.transform.eulerAngles.y, 0);
    }


    private void CheckIfInsideCanvas()
    {
        Vector3[] corners = new Vector3[4];
        rct.GetWorldCorners(corners);

        // foreach (var corner in corners)
        // {
        //    // Debug.Log(corner);
        //     var localSpacePoint = canvas.InverseTransformPoint(corner);
        //    
        //     if (!canvas.rect.Contains(localSpacePoint))
        //     {
        //         Debug.Log("Esta fuera");
        //         Debug.Log(corner);
        //     }
        // }


        foreach (var corner in corners)
        {
            var localSpacePoint = _camera.WorldToViewportPoint(corner);
            localSpacePoint.z = 0;
           // Debug.Log(localSpacePoint);
            bool inCameraFrustum = Is01(localSpacePoint.x) && Is01(localSpacePoint.y);
            
            if (!inCameraFrustum)
            {
                float distance = Vector3.Distance(_camera.transform.position, corner);
                 Vector3 directionBetween = _camera.transform.position- corner ;
                 // directionBetween = directionBetween.normalized;
                 // directionBetween.z = 0;
                 pivot.transform.localPosition += directionBetween;
                 Debug.Log("Direccion: "+ directionBetween);
                 Debug.Log("Pos: "+ pivot.transform.localPosition);
                 pivot.transform.Translate(directionBetween,Space.Self);
                // localSpacePoint.x = 0;
                //
                // var posModify = Camera.main.ViewportToWorldPoint(localSpacePoint);
                // var desplazamiento = corner - posModify;
                // Debug.Log("Movimiento: "+desplazamiento);
                //
                // rct.gameObject.transform.position = posModify;
                 break;
            }
          
           
           

            
        }
        
    }
    public bool Is01(float a) {
        return a > 0 && a < 1;
    }

    public void fitInFrustrum()
    {
        
    }
    
}
