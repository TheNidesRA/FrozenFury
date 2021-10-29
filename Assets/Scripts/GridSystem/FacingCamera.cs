using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    private float fixedRotation;
    void Start()
    {
        fixedRotation = 180f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(fixedRotation, Camera.main.transform.eulerAngles.y, 0);
    }
}
