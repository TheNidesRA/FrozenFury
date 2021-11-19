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
    public float outPosition = 100f;
    private bool _expand=false;
    public RectTransform rect;
    public RectTransform arrowHead;
    private LTDescr actualMovement=null;


    private void Awake()
    {
        
        startPosition = rect.anchoredPosition.x;

    }

    private void OnDisable()
    {
        arrowHead.rotation= Quaternion.Euler(new Vector3(0, 0, 180));
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

    public void setOutOfSceen()
    {
        rect.anchoredPosition = new Vector2(100, 0);
    }

    public void outBuildPosition()
    {
       // 
        actualMovement= LeanTween.moveX(rect, outPosition, duration)
            .setEase(curve)
            .setOnStart(() => { rotate(180);})
            .setOnComplete(()=>
            {
                _expand = false;
                actualMovement = null;
                transform.parent.gameObject.SetActive(false);
            });
    }
    
    
    public void returnInside()
    {
        if (startPosition == rect.anchoredPosition.x) return;
        
       
        arrowHead.rotation= Quaternion.Euler(new Vector3(0, 0, 180));
        
        transform.parent.gameObject.SetActive(true);
        actualMovement= LeanTween.moveX(rect, startPosition, duration)
            .setEase(curve)
            .setOnStart(() =>
            {
                arrowHead.rotation= Quaternion.Euler(new Vector3(0, 0, 180));
                rotate(180);
            })
            .setOnComplete(()=>
            {
                _expand = false;
                actualMovement = null;
                arrowHead.rotation= Quaternion.Euler(new Vector3(0, 0, 0));
            });
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
