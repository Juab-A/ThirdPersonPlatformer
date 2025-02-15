using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;
using System.Numerics;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float gravityMultiplier = 10f;
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashTime = 5f;
    [SerializeField] CinemachineCamera cinemachineCamera;

    private bool isGrounded;
    private Rigidbody rb;   
    private bool canDoubleJump = true; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputManager.OnSpacePressed.AddListener(Jump);
        inputManager.OnMove.AddListener(MovePlayer);
        inputManager.OnShiftPressed.AddListener(Dash);

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isGrounded) {
            rb.AddForce(UnityEngine.Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    private void MovePlayer(UnityEngine.Vector3 direction) {
        rb.AddForce(direction * speed, ForceMode.VelocityChange); 

        //Debug.Log(rb.linearVelocity); Bugging to see the veclocity of the player

        // Cap speed
        UnityEngine.Vector3 horizontalVelocity = new UnityEngine.Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed) {
            UnityEngine.Vector3 cappedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new UnityEngine.Vector3(cappedVelocity.x, rb.linearVelocity.y, cappedVelocity.z);
        }
    }

    
    private void Jump () {
        if (isGrounded){
            ActualJump();

            isGrounded = false;
        } else if (!isGrounded && canDoubleJump) {
            ActualJump();

            canDoubleJump = false;
        }
    }

    private void ActualJump () {
        // Zero out vertical velocity for consistent jumps
            UnityEngine.Vector3 velocity = rb.linearVelocity;
            velocity.y = 0;  // Reset vertical velocity
            rb.linearVelocity = velocity;

            rb.AddForce(UnityEngine.Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
            canDoubleJump = true;
        }
    }

    private void Dash(UnityEngine.Vector3 direction) {
        //Debug.Log("Dash");
        StartCoroutine(ActualDash(direction));
        rb.AddForce(direction.normalized * dashSpeed, ForceMode.Acceleration);
    }

    private IEnumerator ActualDash(UnityEngine.Vector3 direction) {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime) {
            rb.AddForce(direction.normalized * dashSpeed, ForceMode.Acceleration);

            yield return null;
        }
    }
}
