using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update

    private LTDescr tween;
    
    private void OnEnable()
    {
        tween= LeanTween.rotateAround(gameObject, Vector3.up, 360, 20f).setRecursive(true).setEaseShake();
        
    }

    private void OnDisable()
    {
        tween.setRecursive(false);
        LeanTween.cancel(tween.uniqueId);
    }

    void Start()
    {
        

    }
    public float RotateSpeed = 5;
 
    void Update()
    {
       // transform.Rotate(Vector3.right, Time.deltaTime * RotateSpeed);
    }

}
