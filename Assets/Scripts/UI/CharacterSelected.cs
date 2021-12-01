using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{
    public GameObject heladero;
    public GameObject torrine;
    public GameObject coneman;

    public GameObject AudioManager;

    public GameObject heladeroChosen;
    public GameObject torrineChosen;
    public GameObject conemanChosen;

    private Animator characterAnimatorHeladero;
    private Animator characterAnimatorConeman;
    private Animator characterAnimatorTorrine;

    public GameObject[] characters;


    public bool heladeroSelected =  false;
    public bool torrineSelected = false;
    public bool conemanSelected = false;

    public GameObject mainMenu;
    public GameObject Characters;
    public GameObject boton;
    public GameObject Money;

    void Start()
    {
        
        if (PlayerPrefs.GetInt("selectedCharacter") >= 0 && PlayerPrefs.GetInt("selectedCharacter") <= 2)
        {
            int character = PlayerPrefs.GetInt("selectedCharacter");
            if(character == 0)
            {
                heladeroSelected = true;
                heladero.SetActive(heladeroSelected);
                torrine.SetActive(torrineSelected);
                coneman.SetActive(conemanSelected);
            }
            else if(character == 1)
            {
                torrineSelected = true;
                heladero.SetActive(heladeroSelected);
                torrine.SetActive(torrineSelected);
                coneman.SetActive(conemanSelected);
            }
            else if(character == 2)
            {
                conemanSelected = true;
                heladero.SetActive(heladeroSelected);
                torrine.SetActive(torrineSelected);
                coneman.SetActive(conemanSelected);
            }

            characterAnimatorHeladero = heladeroChosen.GetComponent<Animator>();
            characterAnimatorConeman = conemanChosen.GetComponent<Animator>();
            characterAnimatorTorrine = torrineChosen.GetComponent<Animator>();
            //characters[character].SetActive(true);


        }
    
       

        

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


        MonoBehaviour mono = AudioManager.GetComponent<MonoBehaviour>();
        mono.StartCoroutine(celebrationHeladero());




    }

    public IEnumerator celebrationHeladero()
    {
        boton.SetActive(false);
        characterAnimatorHeladero.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.5f);
        characterAnimatorHeladero.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
        
    }

    public IEnumerator celebrationTorrine()
    {
        boton.SetActive(false);
        characterAnimatorTorrine.SetBool("Celebrate", true);
        yield return new WaitForSeconds(2f);
        characterAnimatorTorrine.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
    }

    public IEnumerator celebrationConeman()
    {
        boton.SetActive(false);
        characterAnimatorConeman.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.20f);
        characterAnimatorConeman.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
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
        MonoBehaviour mono = AudioManager.GetComponent<MonoBehaviour>();
        mono.StartCoroutine(celebrationTorrine());
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

        MonoBehaviour mono = AudioManager.GetComponent<MonoBehaviour>();
        mono.StartCoroutine(celebrationConeman());
    }
}
