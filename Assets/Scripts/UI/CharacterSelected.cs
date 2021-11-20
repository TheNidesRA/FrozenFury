using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public GameObject heladero;
    public GameObject torrine;
    public GameObject coneman;

    public GameObject[] characters;


    public bool heladeroSelected =  true;
    public bool torrineSelected = false;
    public bool conemanSelected = false;


    void Start()
    {
        /* characters[0].SetActive(false);
         characters[1].SetActive(false);
         characters[2].SetActive(false);*/
        
        if (PlayerPrefs.GetInt("selectedCharacter") >= 0 && PlayerPrefs.GetInt("selectedCharacter") <= 2)
        {
            int character = PlayerPrefs.GetInt("selectedCharacter");
            characters[character].SetActive(true);
            Debug.Log("HOLA");
            
        }
       /* else
        {
            Debug.Log("ELSE");
            heladero.SetActive(heladeroSelected);
            torrine.SetActive(torrineSelected);
            coneman.SetActive(conemanSelected);
            PlayerPrefs.SetInt("selectedCharacter", 0);
        }*/
       

        

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

        PlayerPrefs.SetInt("selectedCharacter", 0);

    }
    public void OnClickSelectedTorrine()
    {
        heladeroSelected = false;
        torrineSelected = true;
        conemanSelected = false;

        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);

        PlayerPrefs.SetInt("selectedCharacter", 1);
    }
    public void OnClickSelectedConeman()
    {
        heladeroSelected = false;
        torrineSelected = false;
        conemanSelected = true;

        heladero.SetActive(heladeroSelected);
        torrine.SetActive(torrineSelected);
        coneman.SetActive(conemanSelected);

        PlayerPrefs.SetInt("selectedCharacter", 2);
    }
}
