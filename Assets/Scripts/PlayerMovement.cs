using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }

    private const float PLAYER_CUBE_WIDTH = 1f;
    private const float LEVEL_WIDTH = 5f;
    private const float CRITICAL_POS = LEVEL_WIDTH / 2 - PLAYER_CUBE_WIDTH / 2;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float touchSensitivity = .25f;

    [SerializeField] CapsuleCollider bodyCollider;

    private PlayerControls playerControls;

    private float touchDelta = 0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        playerControls = new PlayerControls();
    }

    private void Update()
    {
        HorizontalMovement();
        ApplyMovement();
    }

    public void TouchMovement(InputAction.CallbackContext callbackContext)
    {
        touchDelta = callbackContext.ReadValue<float>();
    }

    private void ApplyMovement()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void HorizontalMovement()
    {
        if (touchDelta == 0) return;

        
        float newPos = transform.position.x + touchDelta * touchSensitivity * Time.deltaTime;

        newPos = Mathf.Clamp(newPos, -CRITICAL_POS, CRITICAL_POS);

        transform.position = new Vector3(newPos, 0, transform.position.z);

    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        //playerControls.Disable();
    }
}
