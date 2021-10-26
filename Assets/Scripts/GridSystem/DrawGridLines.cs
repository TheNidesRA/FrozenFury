using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGridLines : MonoBehaviour
{
    public LineRenderer lr;


    private int index=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setPosition(Vector3 init)
    {
        lr.SetPosition(index,init);
        index++;
    }
}
