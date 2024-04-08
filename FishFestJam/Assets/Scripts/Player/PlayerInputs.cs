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
    bool useAltMove = false;
    private void Awake() {
        playerControls = new();
        playerControls.Movement.Enable();
        
        rb = GetComponent<Rigidbody2D>();
        direction = new();
        UpdateUseAltMove();
    }
    
    private void Update() {
        if(PauseGame.Instance.isGamePaused) { return; }
        LookAtMouse();
        if(useAltMove) { AltMove(); }
        else { Move(); }
    }

    public void UpdateUseAltMove(){
        useAltMove = PlayerPrefs.GetInt("AltCtrl") == 1;
    }
    // Moves with respect of forward/up is where the player is looking
    private void Move(){
        Vector2 newVel = transform.up * playerControls.Movement.Vertical.ReadValue<float>();
        rb.AddForce(newVel * moveSpeed,ForceMode2D.Force);
        Vector2 newVel1 = transform.right * playerControls.Movement.Horizontal.ReadValue<float>();
        rb.AddForce(newVel1 * moveSpeed,ForceMode2D.Force);
    }

    // Moves with respect of forward/up pointing towards the top of the screen
    private void AltMove() {
        float moveVelocityX = playerControls.Movement.Horizontal.ReadValue<float>() * moveSpeed;
        float moveVelocityY = playerControls.Movement.Vertical.ReadValue<float>() * moveSpeed;
        rb.AddForce(new Vector2(moveVelocityX, moveVelocityY),ForceMode2D.Force);
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
