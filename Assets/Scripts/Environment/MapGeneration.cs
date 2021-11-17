using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject[] objPosibles;
    private Quaternion auxRot = Quaternion.identity;
    int aux;
    // Start is called before the first frame update
    void Start()
    {

        aux = Random.Range(0, objPosibles.Length);

        Instantiate(
            objPosibles[aux],
            transform.position,
            auxRot = (aux == 0 || aux == 1 || aux == 2) ? Quaternion.Euler(-90, 0, 0) : Quaternion.identity
            );
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
