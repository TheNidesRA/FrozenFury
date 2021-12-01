using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChooseCanvas : MonoBehaviour
{
    public RectTransform canvas;

    public float time = 0.8f;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void MoveLeft()
    {
     
            
        float x = canvas.localPosition.x;
        canvas.LeanMoveX(x+1920,time).setEase(curve);
    }
    

    // Update is called once per frame
    void Update()
    {
    }
}