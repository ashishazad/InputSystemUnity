using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;
    PlayerInputActions playerInputActions;

    [SerializeField] float speed = 10;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        //playerInput.onActionTriggered += PlayerInput_onActionTriggered;//for c# events

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        playerInputActions.UI.Submit.performed += Submit;
        playerInputActions.UI.Disable();
        //playerInputActions.Player.Movement.performed += Movement;
    }

    private void Update()
    {
        if(Keyboard.current.tKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("UI");
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("Player");
            playerInputActions.UI.Disable();
            playerInputActions.Player.Enable();
        }
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    //private void Movement(InputAction.CallbackContext context)
    //{
    //    Debug.Log(context);
    //    Vector2 movement = context.ReadValue<Vector2>();
    //    rb.AddForce(new Vector3(movement.x,0,movement.y) * speed, ForceMode.Force);
    //}//this was a bad idea because we had to spam the button everytime to move the ball

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log(context);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.phase == InputActionPhase.Performed)
        {
            if (rb.transform.position.y <= 1)
            {
                rb.velocity = new Vector3(0, 5, 0);
            }
        }
    }

    public void Submit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit: "+context);
    }
}
