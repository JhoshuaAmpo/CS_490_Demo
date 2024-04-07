using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputs : MonoBehaviour 
{
    [SerializeField]
    private float turnSpeed = 360f;
    [SerializeField]
    private float moveSpeed = 100f;
    PlayerControls playerControls;
    Rigidbody2D rb;
    Vector3 direction;
    Vector3 velocity;

    private void Awake() {
        playerControls = new();
        playerControls.Movement.Enable();
        
        rb = GetComponent<Rigidbody2D>();
        direction = new();
        velocity = new();
    }

    private void Update() {
        LookAtMouse();
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
        // // Vector2 horizontalVelocity = playerControls.Movement.Horizontal.ReadValue<float>() * transform.right;
        // // Vector2 verticalVelocity =  playerControls.Movement.Vertical.ReadValue<float>() * transform.up;
        // Vector2 inputVec = new(playerControls.Movement.Horizontal.ReadValue<float>(), playerControls.Movement.Vertical.ReadValue<float>());
        // Vector2 forward = (Vector2)transform.right * (Vector2)transform.up * inputVec;
        // Debug.Log($"fwd: {forward}");
        // velocity = forward * moveSpeed;
        // // rb.AddForce(horizontalVelocity + verticalVelocity,ForceMode2D.Force);
        // rb.AddForce(velocity,ForceMode2D.Force);
        Debug.Log($"Up:{transform.up}   Right: {transform.right } ");
        Vector2 newVel = transform.up * playerControls.Movement.Vertical.ReadValue<float>();
        rb.AddForce(newVel * moveSpeed,ForceMode2D.Force);
        Vector2 newVel1 = transform.right * playerControls.Movement.Horizontal.ReadValue<float>();
        rb.AddForce(newVel1 * moveSpeed,ForceMode2D.Force);

    }

    private void LookAtMouse()
    {
        Vector3 screenMousePos = Mouse.current.position.ReadValue();
        screenMousePos.z = 10;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(screenMousePos);
        direction = mousePosition - transform.position;
        float targetAngle = Vector2.SignedAngle(Vector2.up, direction);

        Vector3 targetRotation = new(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), turnSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos() {
        if(transform == null) { return;}
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position+transform.up * 10);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position+transform.right * 10);
    }
}
