using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChooseCanvas : MonoBehaviour
{
    public RectTransform canvas;
    public int index;
    public float time = 0.8f;
    public AnimationCurve curve;
    public List<GameObject> botones;
    public GameObject[] paginasTutorial;

    private event Action OnLeanComplete;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        botones[1].SetActive(true);
        botones[0].SetActive(false);
    }


    private void reactivarBotones()
    {
        foreach(GameObject boton in botones)
        {
            boton.SetActive(true);
        }
    }

    private void desactivarBotones()
    {
        foreach (GameObject boton in botones)
        {
            boton.SetActive(false);
        }
    }

    private void flechasInicioYfin()
    {
        if (index == 0)
            botones[1].SetActive(false);
        else if (index > 0)
            botones[0].SetActive(true);

        if (index == 7)
            botones[0].SetActive(false);
        else if (index < 7)
            botones[1].SetActive(true);
    }

    public void MoveRight()
    {
        // float xOriginal = canvas.
        // float mov = xOriginal  + 1920;
        //
        // Debug.Log("X : "+ xOriginal + " "+mov);
        // LeanTween.moveX(canvas, -mov,time).setEase(curve);
        desactivarBotones();
        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x + -1920, time).setEase(curve).setOnComplete(()=>{ if(index == 0 || index == 7) flechasInicioYfin(); else reactivarBotones();});
        index++;
    }

    public void MoveLeft()
    {
        desactivarBotones();
        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x+1920,time).setEase(curve).setOnComplete(() => { if (index == 0 || index == 7) flechasInicioYfin(); else reactivarBotones(); });
        index--;
    }
    
    public void MoveThrough(int i)
    {
        

        if(i < index) //Caso en el que el boton pulsado es menor que el índice todavía  
        {
            flechasInicioYfin();
            float x = canvas.localPosition.x;
            Debug.Log(i + "Caso i < index");
            Debug.Log(index + "Caso i< index");
            int vPag = index - i;
            float auxV = 1920 * vPag;
            desactivarBotones();
            canvas.LeanMoveX(x + auxV, time).setEase(curve).setOnComplete(() => { if (index == 0 || index == 7) flechasInicioYfin(); else reactivarBotones(); });
            index = i;
        }
        else
        {
            flechasInicioYfin();
            float x = 0;
            Debug.Log(i + "Caso i >= index");
            Debug.Log(index + "Caso i >= index");
            float aux = 1920 * i;
            desactivarBotones();
            canvas.LeanMoveX(x - aux, time).setEase(curve).setOnComplete(() => { if (index == 0 || index == 7) flechasInicioYfin(); else reactivarBotones(); });
            index = i;
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       /* if (index == 0)
            botonIzq.SetActive(false);
        else if(index > 0)
            botonIzq.SetActive(true);

        if (index == 7)
            botonDcho.SetActive(false);
        else if(index < 7)
            botonDcho.SetActive(true);*/

    }
}