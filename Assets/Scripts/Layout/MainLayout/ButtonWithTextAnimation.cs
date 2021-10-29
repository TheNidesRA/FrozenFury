using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonWithTextAnimation : MonoBehaviour,IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    private RectTransform _rcTransform;
    public float increase;
    public float time;
    public GameObject text;
    public string description;

    private TextMeshProUGUI _text;
    private Vector2 _initialSize;
    private LTDescr _animation;
    
    void Start()
    {   
        if(text!=null)
        _text = text.GetComponent<TextMeshProUGUI>();
        _rcTransform = GetComponent<RectTransform>();
        _initialSize = new Vector2(_rcTransform.rect.width, _rcTransform.rect.height);
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
       
        _animation= LeanTween.size(_rcTransform, _initialSize * increase, time);
        if(_text!=null)
        _text.text = description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("WaveGenerator");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animation=  LeanTween.size(_rcTransform, _initialSize,time);
        if(_text!=null)
        _text.text = "";
    }
}
