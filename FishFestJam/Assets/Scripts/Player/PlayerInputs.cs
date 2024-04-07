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

    // Moves with respect of up pointing towards the top of the screen
    // private void Move() {
    //     float moveVelocityX = playerControls.Movement.Horizontal.ReadValue<float>() * moveSpeed;
    //     float moveVelocityY = playerControls.Movement.Vertical.ReadValue<float>() * moveSpeed;
    //     rb.AddForce(new Vector2(moveVelocityX, moveVelocityY),ForceMode2D.Force);
    // }

    // Moves with respect of up is where the player is looking
    private void Move(){
        Vector2 horizontalVelocity = moveSpeed * playerControls.Movement.Horizontal.ReadValue<float>() * transform.right;
        Vector2 verticalVelocity = moveSpeed * playerControls.Movement.Vertical.ReadValue<float>() * transform.up;
        rb.AddForce(horizontalVelocity + verticalVelocity,ForceMode2D.Force);
    }
}
