using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    public bool work = true;
    protected override void Start()
    {
        GridBuildingSystem.Instance.OnSelectedChanged += Aux1;

        GridBuildingSystem.Instance.OnObjectPlaced += Aux2;



        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
        
    }

    private void Aux1(object a, EventArgs b) {


        if (GridBuildingSystem.Instance.buildingSo != null)
        {
            work = false;
            background.gameObject.SetActive(false);
        }
        else
        {
            work = true;
        }

    }
 
    private void Aux2(object a,EventArgs b)
    {
            work = true;
    } 


    private void OnDestroy()
    {
        GridBuildingSystem.Instance.OnSelectedChanged -= Aux1;

        GridBuildingSystem.Instance.OnObjectPlaced -= Aux2;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!work) return;
        base.OnPointerDown(eventData);
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!work) return;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

}