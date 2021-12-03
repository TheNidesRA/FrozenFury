using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelected : MonoBehaviour
{

    public List<GameObject> personajes;

    public GameObject AudioManager;

    public GameObject heladeroChosen;
    public GameObject torrineChosen;
    public GameObject conemanChosen;

    public GameObject heladeroChosenSkin1;
    public GameObject torrineChosenSkin1;
    public GameObject conemanChosenSkin1;

    public GameObject heladeroChosenSkin2;
    public GameObject torrineChosenSkin2;
    public GameObject conemanChosenSkin2;

    private Animator characterAnimatorHeladero;
    private Animator characterAnimatorConeman;
    private Animator characterAnimatorTorrine;

    private Animator characterAnimatorHeladeroSkin1;
    private Animator characterAnimatorConemanSkin1;
    private Animator characterAnimatorTorrineSkin1;

    private Animator characterAnimatorHeladeroSkin2;
    private Animator characterAnimatorConemanSkin2;
    private Animator characterAnimatorTorrineSkin2;

    public bool[] charSelected;

    public GameObject mainMenu;
    public GameObject Characters;
    public GameObject boton;
    public GameObject Money;

    void Start()
    {
        for(int i=0; i<charSelected.Length; i++)
        charSelected[i]= false;

        if (PlayerPrefs.GetInt("selectedCharacter") >= 0 && PlayerPrefs.GetInt("selectedCharacter") <= 8)
        {
            int character = PlayerPrefs.GetInt("selectedCharacter");
            if(character == 0 || character == 3 || character == 4)
            {
                if (character == 0)
                    bucleSetActive(character);
                else if (character == 3)
                    bucleSetActive(character);
                else if (character == 4)
                    bucleSetActive(character);
            }
            else if(character == 1 || character == 5 || character == 6)
            {
                if (character == 1)
                    bucleSetActive(character);
                else if (character == 5)
                    bucleSetActive(character);
                else if (character == 6)
                    bucleSetActive(character);
            }
            else if(character == 2 || character == 7 || character == 8)
            {
                if (character == 2)
                    bucleSetActive(character);
                else if (character == 7)
                    bucleSetActive(character);
                else if (character == 8)
                    bucleSetActive(character);
                
            }

            characterAnimatorHeladero = heladeroChosen.GetComponent<Animator>();
            characterAnimatorConeman = conemanChosen.GetComponent<Animator>();
            characterAnimatorTorrine = torrineChosen.GetComponent<Animator>();

            characterAnimatorHeladeroSkin1 = heladeroChosenSkin1.GetComponent<Animator>();
            characterAnimatorConemanSkin1 = conemanChosenSkin1.GetComponent<Animator>();
            characterAnimatorTorrineSkin1 = torrineChosenSkin1.GetComponent<Animator>();

            characterAnimatorHeladeroSkin2 = heladeroChosenSkin2.GetComponent<Animator>();
            characterAnimatorConemanSkin2 = conemanChosenSkin2.GetComponent<Animator>();
            characterAnimatorTorrineSkin2 = torrineChosenSkin2.GetComponent<Animator>();
        }
    

    }

    private void bucleSetActive(int j)
    {
        for (int i = 0; i < personajes.Count; i++)
        {
            if (i == j)
            {
                charSelected[i] = true;
                personajes[i].SetActive(charSelected[i]);

            }
            else
            {
                charSelected[i] = false;
                personajes[i].SetActive(charSelected[i]);
            }

        }
    }

    public int OnSelectedBefore()
    {
        int aux=0;
        for (int i = 0; i < personajes.Count; i++)
        {
            if (charSelected[i] == true)
                aux = i;
            personajes[i].SetActive(charSelected[i]);
        }

        return aux;
    }

    #region Heladero
    public void OnClickSelectedHeladero()
    {
        MonoBehaviour mono = AudioManager.GetComponent<MonoBehaviour>();
        int aux = OnSelectedBefore();
        if (aux == 0)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationHeladero());
        }
        else if (aux == 3)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationHeladeroSkin1());
        }
        else if (aux == 4)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationHeladeroSkin2());
        }
        else
        {
            bucleSetActive(0);
            PlayerPrefs.SetInt("selectedCharacter", 0);
        }
        
        
        
    }
    public void OnClickSelectedHeladeroOriginal()
    {
        bucleSetActive(0);
        PlayerPrefs.SetInt("selectedCharacter", 0);
    }

    public void OnClickSelectedHeladeroSkin1()
    {
        bucleSetActive(3);
        PlayerPrefs.SetInt("selectedCharacter", 3);
    }
    public void OnClickSelectedHeladeroSkin2()
    {
        bucleSetActive(4);
        PlayerPrefs.SetInt("selectedCharacter", 4);
    }
    #endregion

    #region Torrine
    public void OnClickSelectedTorrine()
    {
        MonoBehaviour mono = AudioManager.GetComponent<MonoBehaviour>();
        int aux = OnSelectedBefore();
        if (aux == 1)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationTorrine());
        }
        else if (aux == 5)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationTorrineSkin1());
        }
        else if (aux == 6)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationTorrineSkin2());
        }
        else
        {
            bucleSetActive(1);
            PlayerPrefs.SetInt("selectedCharacter", 1);
        }
        
        
    }
    public void OnClickSelectedTorrineOriginal()
    {
        bucleSetActive(1);
        PlayerPrefs.SetInt("selectedCharacter", 1);
    }
    public void OnClickSelectedTorrineSkin1()
    {
        bucleSetActive(5);
        PlayerPrefs.SetInt("selectedCharacter", 5);
    }
    public void OnClickSelectedTorrineSkin2()
    {
        bucleSetActive(6);
        PlayerPrefs.SetInt("selectedCharacter", 6);
    }
    #endregion

    #region Coneman
    public void OnClickSelectedConeman()
    {
        MonoBehaviour mono = AudioManager.GetComponent<MonoBehaviour>();
        int aux = OnSelectedBefore();
        if (aux == 2)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationConeman());
        }
        else if (aux == 7)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationConemanSkin1());
        }
        else if (aux == 8)
        {
            bucleSetActive(aux);
            PlayerPrefs.SetInt("selectedCharacter", aux);
            mono.StartCoroutine(celebrationConemanSkin2());
        }
        else
        {
            bucleSetActive(2);
            PlayerPrefs.SetInt("selectedCharacter", 2);
        }
        
       
    }
    public void OnClickSelectedConemanOriginal()
    {
        bucleSetActive(2);
        PlayerPrefs.SetInt("selectedCharacter", 2);
    }
    public void OnClickSelectedConemanSkin1()
    {
        bucleSetActive(7);
        PlayerPrefs.SetInt("selectedCharacter", 7);
    }
    public void OnClickSelectedConemanSkin2()
    {
        bucleSetActive(8);
        PlayerPrefs.SetInt("selectedCharacter", 8);
    }
    #endregion



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

    public IEnumerator celebrationHeladeroSkin1()
    {
        boton.SetActive(false);
        characterAnimatorHeladeroSkin1.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.5f);
        characterAnimatorHeladeroSkin1.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);

    }

    public IEnumerator celebrationHeladeroSkin2()
    {
        boton.SetActive(false);
        characterAnimatorHeladeroSkin2.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.5f);
        characterAnimatorHeladeroSkin2.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);

    }

    public IEnumerator celebrationHeladeroOriginal()
    {
        characterAnimatorHeladero.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.5f);
        characterAnimatorHeladero.SetBool("Celebrate", false);
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

    public IEnumerator celebrationTorrineSkin1()
    {
        boton.SetActive(false);
        characterAnimatorTorrineSkin1.SetBool("Celebrate", true);
        yield return new WaitForSeconds(2f);
        characterAnimatorTorrineSkin1.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
    }

    public IEnumerator celebrationTorrineSkin2()
    {
        boton.SetActive(false);
        characterAnimatorTorrineSkin2.SetBool("Celebrate", true);
        yield return new WaitForSeconds(2f);
        characterAnimatorTorrineSkin2.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
    }

    public IEnumerator celebrationTorrineOriginal()
    {
        characterAnimatorTorrine.SetBool("Celebrate", true);
        yield return new WaitForSeconds(2f);
        characterAnimatorTorrine.SetBool("Celebrate", false);
    }

    public IEnumerator celebrationConeman()
    {
        boton.SetActive(false);
        characterAnimatorConeman.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.7f);
        characterAnimatorConeman.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
    }

    public IEnumerator celebrationConemanSkin1()
    {
        boton.SetActive(false);
        characterAnimatorConemanSkin1.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.7f);
        characterAnimatorConemanSkin1.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
    }

    public IEnumerator celebrationConemanSkin2()
    {
        boton.SetActive(false);
        characterAnimatorConemanSkin2.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.7f);
        characterAnimatorConemanSkin2.SetBool("Celebrate", false);
        Characters.SetActive(false);
        mainMenu.SetActive(true);
        boton.SetActive(true);
        Money.SetActive(true);
    }
    public IEnumerator celebrationConemanOriginal()
    {
        characterAnimatorConeman.SetBool("Celebrate", true);
        yield return new WaitForSeconds(1.20f);
        characterAnimatorConeman.SetBool("Celebrate", false);
    }



}
