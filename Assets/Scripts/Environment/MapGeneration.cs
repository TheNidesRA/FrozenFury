using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] objPosibles;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(
            objPosibles[Random.Range(0, objPosibles.Length)],
            transform.position,
            Quaternion.identity
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
