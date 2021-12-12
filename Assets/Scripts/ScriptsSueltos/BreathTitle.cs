using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathTitle : MonoBehaviour
{
    public enum AnimationType
    {
        Breath,
        Shake,
        Both
    }


    public AnimationType animationType;
    public AnimationCurve curve;
    public float duration;
    public float delay;
    public float ammount;
    private bool _expand = false;
    private RectTransform rect = null;


    public LTDescr actualMovement = null;

    private Vector3 initPosition;
    private Vector3 initRotation;
    private Vector2 initScale;

    public bool loop = true;

    private void Awake()
    {
    }

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        initPosition = rect.position;
        initRotation = rect.rotation.eulerAngles;
        initScale = rect.sizeDelta;
    }


    private void OnEnable()
    {
        if (!ReferenceEquals(rect, null))
        {
            rect.position = initPosition;
            rect.rotation = Quaternion.Euler(initRotation);
            rect.sizeDelta = initScale;
            if (loop)
                switch (animationType)
                {
                    case AnimationType.Breath:

                        actualMovement = LeanTween.size(rect, rect.sizeDelta * ammount, duration).setRecursive(true)
                            .setEase(curve);
                        break;
                    case AnimationType.Shake:
                        actualMovement = LeanTween.rotateAround(rect, Vector3.forward, ammount, duration)
                            .setRecursive(true)
                            .setEase(curve);

                        break;
                }
        }
    }


    public void StartTweening()
    {
        switch (animationType)
        {
            case AnimationType.Breath:

                actualMovement = LeanTween.size(rect, rect.sizeDelta * ammount, duration)
                    .setEase(curve);
                break;
            case AnimationType.Shake:
                actualMovement = LeanTween.rotateAround(rect, Vector3.forward, ammount, duration)
                    .setEase(curve);

                break;
            case AnimationType.Both:
                actualMovement = LeanTween.rotateAround(rect, Vector3.forward, ammount, duration)
                    .setEase(curve);
                actualMovement = LeanTween.size(rect, rect.sizeDelta * ammount, duration)
                    .setEase(curve);
                break;
        }
    }

    private void OnDisable()
    {
        if (!ReferenceEquals(actualMovement, null))
            if (LeanTween.isTweening(actualMovement.id))
            {
                actualMovement.setRecursive(false);
                actualMovement.setTime(0);
                LeanTween.cancel(rect);
                LeanTween.cancel(actualMovement.id);
            }
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