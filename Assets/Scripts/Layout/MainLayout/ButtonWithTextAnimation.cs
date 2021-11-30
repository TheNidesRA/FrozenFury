using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonWithTextAnimation : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private RectTransform _rcTransform;
    public float increase = 1.2f;
    public float time = 0.3f;
    public GameObject text;
    public string description;


    private TextMeshProUGUI _text;
    private Vector2 _initialSize;
    private LTDescr _animation;
    private Button btn = null;
    public bool dishableOnPress;
    public bool activarOnEnable = true;

    private void Awake()
    {
        if (text != null)
            _text = text.GetComponent<TextMeshProUGUI>();
        _rcTransform = GetComponent<RectTransform>();
        _initialSize = new Vector2(_rcTransform.rect.width, _rcTransform.rect.height);
        TryGetComponent(out btn);
    }


    private void OnEnable()
    {
        _rcTransform.sizeDelta = _initialSize;
        if (!ReferenceEquals(btn, null))
        {
            if (!btn.interactable && activarOnEnable)
                btn.interactable = true;
        }
    }

    private void OnDisable()
    {
        _rcTransform.sizeDelta = _initialSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animation = LeanTween.size(_rcTransform, _initialSize * increase, time);
        if (_text != null)
            _text.text = description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (dishableOnPress && !ReferenceEquals(btn, null))
        {
            if (btn.interactable)
                btn.onClick.Invoke();
            btn.interactable = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animation = LeanTween.size(_rcTransform, _initialSize, time);
        if (_text != null)
            _text.text = "";
    }
}