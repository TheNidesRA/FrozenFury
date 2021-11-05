using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoyStickController : MonoBehaviour
{

    private InputPlayer _control;


    private void OnEnable()
    {
        _control.Enable();
    }

    private void OnDisable()
    {
        _control.Disable();
    }
    private static JoyStickController instance;
    public static JoyStickController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<JoyStickController>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("OnScreenStick");
                    instance = instanceContainer.AddComponent<JoyStickController>();
                }
            }
            return instance;
        }
    }

    public GameObject smallStick;
    public GameObject bgStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVector;
    float stickRadius;
    Vector3 joyStickFirstPosition;

    private void Awake()
    {
        _control = new InputPlayer();
        _control.PlayerMain.Movement.performed += OnDrag;
        _control.PlayerMain.Movement.performed += OnPointerDown;
        _control.PlayerMain.Movement.canceled += OnPointerUp;
        _control.PlayerMain.Movement.canceled += OnDrop;
    }
    private void Start()
    {
        stickRadius = bgStick.gameObject.GetComponent<RectTransform>().sizeDelta.y/2;
        joyStickFirstPosition = bgStick.transform.position;
    }

    public void OnPointerDown(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action == null)
            throw new System.ArgumentNullException(nameof(callbackContext.action.name));

        bgStick.transform.position = _control.PlayerMain.Movement.ReadValue<Vector2>();
        smallStick.transform.position = _control.PlayerMain.Movement.ReadValue<Vector2>();
        stickFirstPosition = _control.PlayerMain.Movement.ReadValue<Vector2>();

    }

    public void OnDrag(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action == null)
            throw new System.ArgumentNullException(nameof(callbackContext.action.name));

        Vector3 DragPosition = _control.PlayerMain.Movement.ReadValue<Vector2>();
        joyVector = (DragPosition - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);

        if (stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joyVector * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joyVector * stickRadius;
        }
    }

    public void OnPointerUp(InputAction.CallbackContext callbackContext)
    {
        joyVector = Vector3.zero;
        bgStick.transform.position = joyStickFirstPosition;
        smallStick.transform.position = joyStickFirstPosition;

    }
    public void OnDrop(InputAction.CallbackContext callbackContext)
    {
        joyVector = Vector3.zero;
        bgStick.transform.position = joyStickFirstPosition;
        smallStick.transform.position = joyStickFirstPosition;

    }

   

    /*  public float movementRange
      {
          get => m_MovementRange;
          set => m_MovementRange = value;
      }

      [FormerlySerializedAs("movementRange")]
      [SerializeField]
      private float m_MovementRange = 50;

      [InputControl(layout = "Vector2")]


      private Vector3 m_StartPos;
      private Vector2 m_PointerDownPos;
    */

}
