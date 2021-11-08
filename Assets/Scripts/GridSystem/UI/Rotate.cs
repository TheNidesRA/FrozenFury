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
        tween= LeanTween.rotateAround(gameObject, Vector3.up, 360, 20f).setLoopClamp().setEaseShake();
        
    }

    private void OnDisable()
    {
        tween.setLoopClamp(0);
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
