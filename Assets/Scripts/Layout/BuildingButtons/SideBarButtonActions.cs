using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarButtonActions : MonoBehaviour
{

    public AnimationCurve curve;
    public float duration;
    public float delay;
    public LeanTweenType easeType;
    public float startPosition;
    public float endPosition;
    private bool _expand=false;
    public RectTransform rect;
    public RectTransform arrowHead;
    private LTDescr actualMovement=null;


    private void Start()
    {
        startPosition = rect.anchoredPosition.x;

    }

    public void buttonClick()
    {
        if (actualMovement == null)
        {
            if (!_expand)
            {
                actualMovement =LeanTween.moveX(rect, endPosition, duration)
                    .setEase(curve)
                    .setOnStart(() => { rotate(180);})
                    .setOnComplete(()=>{
                        changeExpand();
                        actualMovement = null;
                    });
                
            }
            else
            {
                actualMovement= LeanTween.moveX(rect, startPosition, duration)
                    .setEase(curve)
                    .setOnStart(() => { rotate(180);})
                    .setOnComplete(()=>{
                        changeExpand();
                        actualMovement = null;
                    });
            }
        }
        
        
    }


    private void rotate(float angle)
    {
        LeanTween.rotateAround(arrowHead, new Vector3(0, 0, 1), angle, duration);
    }
    
    private void changeExpand()
    {
        _expand = !_expand;
    }
    
}
