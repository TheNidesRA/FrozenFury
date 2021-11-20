using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public GameObject heladero;
    public GameObject torrine;
    public GameObject coneman;

    // Start is called before the first frame update
    void Start()
    {
        heladero.SetActive(true);
        torrine.SetActive(false);
        coneman.SetActive(false);

    }
    public void OnClickSelectedHeladero()
    {
        heladero.SetActive(true);
        torrine.SetActive(false);
        coneman.SetActive(false);
    }
    public void OnClickSelectedTorrine()
    {
        heladero.SetActive(false);
        torrine.SetActive(true);
        coneman.SetActive(false);
    }
    public void OnClickSelectedConeman()
    {
        heladero.SetActive(false);
        torrine.SetActive(false);
        coneman.SetActive(true);
    }
}
