using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{

    [Tooltip("Mouse input to move camera")]
    [SerializeField] InputAction mouseInput;
    [Tooltip("Player object to follow")]
    [SerializeField] GameObject player;
    [Tooltip("distance between player and camera")]
    [SerializeField] Vector3 offset;
    [Tooltip("Smoothnes of camera moving to new location")]
    [SerializeField] float damp;


    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FollowPlayer()
    {
    }

    private Vector3 CalculateOffsetBasedOnPlayerPosition()
    {

        return Vector3.Reflect(player.transform.forward, offset);
    }
}
