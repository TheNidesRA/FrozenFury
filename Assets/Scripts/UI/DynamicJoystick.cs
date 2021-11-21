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
        GridBuildingSystem.Instance.OnSelectedChanged += (a, b) =>
        {
            if (GridBuildingSystem.Instance.buildingSo != null)
            {
                work = false;
                background.gameObject.SetActive(false);
            } else
            {
                work = true;
            }

            
            
           
            
        };
       
        GridBuildingSystem.Instance.OnObjectPlaced += (a, b) =>
        {
            work = true;
        } ;




        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
        
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