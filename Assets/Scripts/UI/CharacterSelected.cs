using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public GameObject heladero;
    public GameObject torrine;
    public GameObject coneman;

    public  bool heladeroSelected =  true;
    public  bool torrineSelected = false;
    public bool conemanSelected = false;


    void Start()
    {
        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);

    }

    public void OnSelectedBefore()
    {
        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);
    }

    public void OnClickSelectedHeladero()
    {
        heladeroSelected = true;
        torrineSelected = false;
        conemanSelected = false;

        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);
    }
    public void OnClickSelectedTorrine()
    {
        heladeroSelected = false;
        torrineSelected = true;
        conemanSelected = false;

        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);
    }
    public void OnClickSelectedConeman()
    {
        heladeroSelected = false;
        torrineSelected = false;
        conemanSelected = true;

        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);
    }
}
