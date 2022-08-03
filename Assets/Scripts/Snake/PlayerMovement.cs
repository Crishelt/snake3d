using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 8f;
    public float MovementSpeed { get { return movementSpeed; } }
    [SerializeField] float delayBetweenRotation = 0.15f;
    [SerializeField] InputAction playerMovement;

    float lastRotationTime;

    private void Start()
    {
        lastRotationTime = Time.time;
    }

    private void OnEnable()
    {
        playerMovement.Enable();
    }

    private void OnDisable()
    {
        playerMovement.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveForward();
        ProcessRotation();
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    private bool CanRotate()
    {
        return lastRotationTime + delayBetweenRotation <= Time.time;
    }

    private void ProcessRotation()
    {
        if (!CanRotate() || !playerMovement.triggered) return;

        Vector2 movement = playerMovement.ReadValue<Vector2>();
        lastRotationTime = Time.time;
        if (Mathf.Abs(movement.x) > 0f)
        {
            //rotates left or right
            transform.Rotate(0, (90 * Mathf.Sign(movement.x)), 0);
        }
        else if (Mathf.Abs(movement.y) > 0f)
        {
            //rotates up or down (WS keys)
            transform.Rotate((90 * -1 * Mathf.Sign(movement.y)), 0, 0);
        }

        BroadcastMessage("HeadChangedRotation", SendMessageOptions.DontRequireReceiver);
    }
}
