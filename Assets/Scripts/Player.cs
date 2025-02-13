using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private int speed;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager.OnMove.AddListener(MovePlayer);
        rb = GetComponent<Rigidbody>();
    }

    private void MovePlayer(Vector2 direction) {
        Vector3 moveDirection = new (direction.x, 0f, direction.y);
        rb.AddForce(moveDirection * speed);
    }
}
