using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathTitle : MonoBehaviour
{
    public enum AnimationType
    {
        Breath,
        Shake
    }


    public AnimationType animationType;
    public AnimationCurve curve;
    public float duration;
    public float delay;
    public float ammount;
    private bool _expand = false;
    private RectTransform rect;


    private LTDescr actualMovement = null;

    private Vector3 initPosition;
    private Vector3 initRotation;
    private Vector2 initScale;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        initPosition = rect.position;
        initRotation = rect.rotation.eulerAngles;
        initScale = rect.sizeDelta;
    }


    private void OnEnable()
    {
        switch (animationType)
        {
            case AnimationType.Breath:
          
                actualMovement = LeanTween.size(rect, rect.sizeDelta*ammount,  duration).setLoopClamp().setEase(curve);
                break;
            case AnimationType.Shake:
                actualMovement = LeanTween.rotateAround(rect, Vector3.forward, ammount ,duration).setLoopClamp().setEase(curve);
            
                break;
        }
    }

    private void OnDisable()
    {
        actualMovement.setLoopClamp(0);
    }


    private void infiniteLoop()
    {
    }

    // public void buttonClick()
    // {
    //     if (actualMovement == null)
    //     {
    //         if (!_expand)
    //         {
    //             actualMovement = LeanTween.moveX(rect, endPosition, duration)
    //                 .setEase(curve)
    //                 .setOnStart(() => { rotate(180); })
    //                 .setOnComplete(() =>
    //                 {
    //                     changeExpand();
    //                     actualMovement = null;
    //                 });
    //         }
    //         else
    //         {
    //             actualMovement = LeanTween.moveX(rect, startPosition, duration)
    //                 .setEase(curve)
    //                 .setOnStart(() => { rotate(180); })
    //                 .setOnComplete(() =>
    //                 {
    //                     changeExpand();
    //                     actualMovement = null;
    //                 });
    //         }
    //     }
    // }


    private void changeExpand()
    {
        _expand = !_expand;
    }
}