using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChooseCanvas : MonoBehaviour
{
    public RectTransform canvas;
    public int index;
    public float time = 0.8f;
    public AnimationCurve curve;
    public List<GameObject> flechas;
    public GameObject[] paginasTutorial;
    public GameObject[] botones;
    public Sprite indiceActualIMG,indicePrevioIMG;
    
    private event Action OnLeanComplete;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        flechas[1].SetActive(true);
        flechas[0].SetActive(false);
        botones[index].GetComponent<Image>().sprite = indiceActualIMG;
    }


    private void reactivarBotones()
    {
        foreach(GameObject boton in flechas)
        {
            boton.SetActive(true);
        }
    }

    private void desactivarBotones()
    {
        foreach (GameObject boton in flechas)
        {
            boton.SetActive(false);
        }
    }

    private void flechasInicio()
    {
        if (index == 0)
        {
            flechas[1].SetActive(true);
            flechas[0].SetActive(false);
        }
        else if (index > 0)
            flechas[0].SetActive(true);
    }

    private void flechasfin()
    { 
        if (index == 7)
        {
            flechas[1].SetActive(false);
            flechas[0].SetActive(true);
        }
        else if (index < 7)
            flechas[1].SetActive(true);
    }

    private void controlFlechas() {
        bool fInicio = false, fFin = false;

        if (index == 0)
        {
            flechasInicio();
            fInicio = true;
        }
            
        if (index == 7)
        {
            flechasfin();
            fFin = true;
        }
           
        if(!fInicio &&  !fFin)
        reactivarBotones(); 
    }


    public void MoveRight()
    {
        desactivarBotones();
        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x + -1920, time).setEase(curve).setOnComplete(() => { controlFlechas(); });

        //Indice de paginacion tutorial
        botones[index].GetComponent<Image>().sprite = indicePrevioIMG;
        index++;
        botones[index].GetComponent<Image>().sprite = indiceActualIMG;
    }

    public void MoveLeft()
    {
        desactivarBotones();
        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x+1920,time).setEase(curve).setOnComplete(() => { controlFlechas(); });

        //Indice de paginacion tutorial
        botones[index].GetComponent<Image>().sprite = indicePrevioIMG;
        index--;
        botones[index].GetComponent<Image>().sprite = indiceActualIMG;
    }
    
    public void MoveThrough(int i)
    {

        if(i < index) //Caso en el que el boton pulsado es menor que el índice todavía  
        {

            flechasInicio();
            flechasfin();
            float x = canvas.localPosition.x;
            Debug.Log(i + "Caso i < index");
            Debug.Log(index + "Caso i< index");
            int vPag = index - i;
            float auxV = 1920 * vPag;
            desactivarBotones();
            canvas.LeanMoveX(x + auxV, time).setEase(curve).setOnComplete(() => { controlFlechas(); });

            //Indice de paginacion tutorial
            botones[index].GetComponent<Image>().sprite = indicePrevioIMG;
            index = i;
            botones[index].GetComponent<Image>().sprite = indiceActualIMG;
        }
        else
        {
            flechasInicio();
            flechasfin();
            float x = 0;
            Debug.Log(i + "Caso i >= index");
            Debug.Log(index + "Caso i >= index");
            float aux = 1920 * i;
            desactivarBotones();
            canvas.LeanMoveX(x - aux, time).setEase(curve).setOnComplete(() => { controlFlechas(); });
            
            //Indice de paginacion tutorial
            botones[index].GetComponent<Image>().sprite = indicePrevioIMG;
            index = i;
            botones[index].GetComponent<Image>().sprite = indiceActualIMG;
        }

        
    }

}