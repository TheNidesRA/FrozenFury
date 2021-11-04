using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, 20f).setLoopClamp().setEaseShake();

    }
    public float RotateSpeed = 5;
 
    void Update()
    {
       // transform.Rotate(Vector3.right, Time.deltaTime * RotateSpeed);
    }

}
