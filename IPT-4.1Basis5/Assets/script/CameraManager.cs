using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;
    UI ui;
    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform cameraTransform;
    private float defaultposition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    public float cameraCollisionRadios = 0.2f;
    public float lookAngle;
    public float privotAngle;
    public float minPivotAngle = -20;
    public float maxPivotAngle = 35;
    public LayerMask collisionLayers;
    public float cameraFollowSpeed = 0.1f;
    public float cameraLookSpeed = 0.4f;
    public float cameraPivotSpeed = 0.4f;
    public float cameraCollisionOffSet = 0.2f;
    public float minCollsionOffset = 0.2f;
    private Vector3 cameraVectorPosition;

    private void Awake()
    {
        ui = FindObjectOfType<UI>();
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultposition = cameraTransform.localPosition.z;
    }
    public void HandleAllCameraMovement()
    {
        FollowAstronout();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void FollowAstronout()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }
    private void RotateCamera()
    {
        if (ui.menu_active)
        {
            return;
        }
        Vector3 rotation;
        Quaternion targetRotation;
        lookAngle = lookAngle + (inputManager.cameraInputY * cameraLookSpeed);
        privotAngle = privotAngle - (inputManager.cameraInputX * cameraPivotSpeed);
        privotAngle = Mathf.Clamp(privotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = privotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
    private void HandleCameraCollisions()
    {
        float targetPosition = defaultposition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadios, direction, out hit, Mathf.Abs(targetPosition), collisionLayers)) {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = targetPosition - (distance - cameraCollisionOffSet);

        }

        if (Mathf.Abs(targetPosition) < minCollsionOffset)
        {
            targetPosition = targetPosition - minCollsionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
