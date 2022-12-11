using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    Vector3 moveDirection;

    [Header("Generell")]
    Transform cameraObject;
    Rigidbody Astronout;

    [Header("Falling")]
    public bool isGrounded;
    public bool issprinting;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;
    public float fallingSpeed;
    public float leapingVelocity;
    public float inAirTimer;

    [Header("Movement Speeds")]
    public float walkingSpeed = 3;
    public float runningspeed = 7;
    public float sprintingspeed = 2;
    public float rotationspeed = 8;

    [Header("Movement")]
    public bool isJumping;
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    public void FixedUpdate()
    {
        if (transform.position.y < -25)
            GetComponent<serialize>().reset_world();
    }

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        Astronout = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
        {
            return;
        }
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        if (isJumping)
        {
            return;
        }
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        
        if (issprinting)
        {
            moveDirection = moveDirection * sprintingspeed;
        }
        else
        {
            moveDirection = moveDirection * walkingSpeed * 4;
        }

        Vector3 movementVelocity = moveDirection;
        Astronout.velocity = movementVelocity;


    }

    private void HandleRotation()
    {
        if (isJumping)
            return;
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationspeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            Astronout.AddForce(transform.forward * leapingVelocity);
            Astronout.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
        }

        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", false);
            animatorManager.PlayTargetAnimation("Jump", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            Astronout.velocity = playerVelocity;
            
        }
    }
}
