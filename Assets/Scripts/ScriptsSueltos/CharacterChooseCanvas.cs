using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChooseCanvas : MonoBehaviour
{
    public RectTransform canvas;
    public int index;
    public float time = 0.8f;
    public AnimationCurve curve;
    public GameObject botonIzq, botonDcho;
    public GameObject[] paginasTutorial;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        botonDcho.SetActive(true);
        botonIzq.SetActive(false);
    }

    public void MoveRight()
    {
        // float xOriginal = canvas.
        // float mov = xOriginal  + 1920;
        //
        // Debug.Log("X : "+ xOriginal + " "+mov);
        // LeanTween.moveX(canvas, -mov,time).setEase(curve);

        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x+-1920,time).setEase(curve);
        index++;
    }

    public void MoveLeft()
    {

        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x+1920,time).setEase(curve);
        index--;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (index == 0)
            botonIzq.SetActive(false);
        else if(index > 0)
            botonIzq.SetActive(true);

        if (index == 7)
            botonDcho.SetActive(false);
        else if(index < 7)
            botonDcho.SetActive(true);

    }
}