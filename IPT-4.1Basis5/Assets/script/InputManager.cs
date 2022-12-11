using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    PlayerLocomotion playerLocomotion;

    public float cameraInputX;
    public float cameraInputY;

    
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool shift_Input;
    public bool jump_Input;
    public bool p_Input;
    

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => shift_Input = true;
            playerControls.PlayerActions.Sprint.canceled += i => shift_Input = false;
            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

            playerControls.ButtonAction.PauseMenu.performed += i => p_Input = true;
            playerControls.ButtonAction.PauseMenu.canceled += i => p_Input = false;
        }

        

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        SprintingInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.y;
        cameraInputY = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, shift_Input);

    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SprintingInput()
    {
        if (shift_Input)
        {
            playerLocomotion.issprinting = true;
        }
        else
        {
            playerLocomotion.issprinting = false;
        }

    }
    private void HandleJumpingInput()
    {
        if (jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();          
        }
    }
}
