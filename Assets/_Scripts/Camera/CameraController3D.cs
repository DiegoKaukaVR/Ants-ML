using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController3D : MonoBehaviour
{
    PlayerCamera playerCamera;
    InputAction movement;
    Transform cameraTransform;

    [SerializeField]
    float maxSpeed = 5f;
    float speed;
    [SerializeField]
    float acceleration = 10f;
    float damping = 15f;

    [SerializeField]
    float stepSize = 2f;
    [SerializeField]
    float zoomdampening = 7.5f;
    [SerializeField]
    float minHeight = 5f;
    [SerializeField]
    float maxHeight = 50f;
    [SerializeField]
    float zoomSpeed = 2f;

    [SerializeField]
    float maxRotationSpeed = 1f;

    [SerializeField, Range(0f, 0.1f)]
    float edgeTolerance = 0.05f;
    [SerializeField]
    bool useScreenEdge = true;

    Vector3 targetPosition;
    float zoomHeight;

    Vector3 horizontalVelocity;
    Vector3 lastPosition;

    Vector3 startDrag;

    private void Awake()
    {
        playerCamera = new PlayerCamera();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(transform);

        lastPosition = transform.position;
        movement = playerCamera.Move.InputMove;
        playerCamera.Move.Zoom.performed += ZoomCamera;
        playerCamera.Move.Enable();
    }

    private void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        float value = -inputValue.ReadValue<Vector2>().y / 100f;

        if (Mathf.Abs(value) > 0.1f)
        {
            zoomHeight = cameraTransform.localPosition.y + value * stepSize;

            if (zoomHeight < minHeight)
            {
                zoomHeight = minHeight;
            }
            else if(zoomHeight > maxHeight)
            {
                zoomHeight = maxHeight;
            }
            
        }
    }

    private void UpdateCameraPosition()
    {
        //set zoom target
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        //add vector for forward/backward zoom
        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomdampening);
        cameraTransform.LookAt(this.transform);
    }

    private void OnDisable()
    {
        playerCamera.Move.Disable();

    }

    private void Update()
    {
        GetKeyboardMovement();
        UpdateVelocity();
        UpdateCameraPosition();
        UpdateBasePosition();
    }
    void UpdateVelocity()
    {
        horizontalVelocity = (this.transform.position - lastPosition) / Time.deltaTime;
        horizontalVelocity.y = 0;
        lastPosition = transform.position;
    }

    void GetKeyboardMovement()
    {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight() + movement.ReadValue<Vector2>().y * GetCameraForward();

        inputValue = inputValue.normalized;

        if (inputValue.sqrMagnitude > 0.1f)
        {
            targetPosition += inputValue;
        }
    }

    Vector3 GetCameraRight()
    {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        return right;
    }
    Vector3 GetCameraForward()
    {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    void UpdateBasePosition()
    {
        if (targetPosition.sqrMagnitude > 0.1f)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else
        {
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * damping);
            transform.position += horizontalVelocity * Time.deltaTime;
        }

        targetPosition = Vector3.zero;
    }




}
