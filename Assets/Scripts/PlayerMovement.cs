using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float moveSpeed = 1f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ForwardMovement();
    }

    private void ForwardMovement()
    {
        characterController.Move(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
