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
            Debug.Log("HOLLAAA");
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

   /* protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            //background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }*/
}