using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] CinemachineCamera cinemachineCamera;

    private Rigidbody rb;
    private Transform playerDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
        playerDirection = GetComponent<Transform>();
    }

    private void MovePlayer(Vector3 direction) {
        //Vector3 moveDirection = direction;

        rb.AddForce(direction.normalized * speed, ForceMode.VelocityChange); 

        Debug.Log(rb.linearVelocity);

        // Cap speed
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed) {
            Vector3 cappedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(cappedVelocity.x, rb.linearVelocity.y, cappedVelocity.z);
        }
    }
}
