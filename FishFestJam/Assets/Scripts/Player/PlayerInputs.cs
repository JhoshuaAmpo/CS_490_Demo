using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputs : MonoBehaviour 
{
    PlayerControls playerControls;
    Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 100f;

    private void Awake() {
        playerControls = new();
        playerControls.Movement.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        Move();
    }

    private void Move() {
        float moveVelocityX = playerControls.Movement.Horizontal.ReadValue<float>() * moveSpeed;
        float moveVelocityY = playerControls.Movement.Vertical.ReadValue<float>() * moveSpeed;
        rb.AddForce(new Vector2(moveVelocityX, moveVelocityY),ForceMode2D.Force);
    }
}
