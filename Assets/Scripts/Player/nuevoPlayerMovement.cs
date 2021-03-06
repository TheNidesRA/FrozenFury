using System;
using System.Collections;
using System.Collections.Generic;
using GridSystem;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class nuevoPlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private InputPlayer inputPlayer;

    private Animator characterAnimator;

    private Vector2 inputMovement;
    private Vector3 v_movement;
    private Vector3 v_velocity;
    public float v_movement_z;
    public float v_movement_x;
    public float moveSpeed;
    private float gravity;
    private int rotationSpeed;
    public Joystick joystickDynamic;

    public bool estadoAnterior = false;
    public bool work = true;

    public static bool controlMovimiento;
    // Start is called before the first frame update
    private void Awake()
    {
        work = true;
        estadoAnterior = false;
        inputPlayer = new InputPlayer();
        _characterController = GetComponent<CharacterController>();
        v_movement_z = v_movement.z;
        v_movement_x = v_movement.x;

        characterAnimator = GetComponent<Animator>();

        controlMovimiento = true;
        moveSpeed = 35f;
        gravity = 0.5f;
        rotationSpeed = 720;
        //inputPlayer.PlayerMain.Movement.performed += Movement_performed;
    }

    /*  private void Movement_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
      {
          inputMovement = obj.ReadValue<Vector2>();
      }
     */
    void Start()
    {
        GridBuildingSystem.Instance.OnSelectedChanged += Aux1;
        GridBuildingSystem.Instance.OnObjectPlaced += Aux2;
        GridBuildingSystem.Instance.OnObjectSetPosition += Aux3;
    }

    private void Aux1(object a, EventArgs b)
    {


        if (GridBuildingSystem.Instance.buildingSo != null)
        {
            work = false;
        }
        else
        {
            work = true;
        }

    }

    private void OnDestroy()
    {
        GridBuildingSystem.Instance.OnSelectedChanged -= Aux1;
        GridBuildingSystem.Instance.OnObjectPlaced -= Aux2;
        GridBuildingSystem.Instance.OnObjectSetPosition -= Aux3;
    }

    private void Aux2(object a, EventArgs b)
    {
        work = true;
    }

    private void Aux3(object a, EventArgs b)
    {
        work = false;
    }

    private void OnEnable()
    {
        inputPlayer.Enable();
    }
    private void OnDisable()
    {
        inputPlayer.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!work)
        {
            if (joystickDynamic != null)
                joystickDynamic.enabled = false;
            estadoAnterior = true;
            inputMovement = new Vector2(0, 0);
            return;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            inputMovement = inputPlayer.PlayerMain.Movement.ReadValue<Vector2>();
        }
        else
        {
            if (!estadoAnterior)
            {
                inputMovement = new Vector2(joystickDynamic.Horizontal, joystickDynamic.Vertical);
            }
            else
            {
                joystickDynamic.OnDisable();
                inputMovement = new Vector2(joystickDynamic.Horizontal, joystickDynamic.Vertical);
                estadoAnterior = false;
                
            }
            
        }

    }

    private void FixedUpdate()
    {
        if (_characterController.isGrounded)
        {
            v_velocity.y = 0f;
        }
        else
        {
            v_velocity.y -= gravity * Time.deltaTime;
        }


        v_movement.z = inputMovement.y;
        v_movement.x = inputMovement.x;

        v_movement_z = v_movement.z;
        v_movement_x = v_movement.x;

        characterAnimator.SetFloat("MoveZ", v_movement.z);
        characterAnimator.SetFloat("MoveX", v_movement.x);


        if (inputMovement == Vector2.zero)
        {

        }
        else
        {

            if (controlMovimiento)
            {
                //Debug.Log("No deber??a de haber enemigos");
                Quaternion toRotation = Quaternion.LookRotation(v_movement, Vector3.up);
                _characterController.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            }

            _characterController.Move(v_movement * moveSpeed * Time.deltaTime);
            _characterController.Move(v_velocity);
        }

    }
}
