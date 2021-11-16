using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{

    public int rotationScale = 10;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);

    }
}
