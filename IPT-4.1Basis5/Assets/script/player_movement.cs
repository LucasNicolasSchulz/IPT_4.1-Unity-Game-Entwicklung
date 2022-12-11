using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class player_movement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            animator.SetBool("air", false);
            // We are grounded, so recalculate
            // move direction directly from axes
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 6;
                animator.SetFloat("sprint", 1.5f);
            }
            else
            {
                speed = 4;
                animator.SetFloat("sprint", 1f);
            }

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                animator.SetBool("air", true);
            }

            if(Input.GetKey(KeyCode.W))
            {
                animator.SetBool("walking", true);
            }
            else
            {
                animator.SetBool("walking", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("walking", true);
                animator.SetFloat("sprint", -1f);
            }
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        if(transform.position.y < -25)
        {
            transform.position = new Vector3(0, 10, 0);
        }

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetBool("interaction", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("interaction", false);
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }
}
