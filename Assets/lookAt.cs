using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class lookAt : MonoBehaviour
{
    private Vector3 fixedrot;

    private void Awake()
    {
       fixedrot = Camera.main.transform.forward;
        
       
    }

    void Update()
    {
        // transform.forward = Camera.main.transform.forward;
        // gameObject.transform.rotation = fixedrot;
        transform.forward = fixedrot;
    }

}
