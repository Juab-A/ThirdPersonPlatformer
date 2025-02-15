using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera cinemachineCamera;
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent OnSpacePressed = new UnityEvent();
    private Transform transformCamera;

    void Start()
    {
        transformCamera = cinemachineCamera.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {

        }

        Vector3 input = Vector3.zero;

        // Flatten and normalize the camera directions
        Vector3 flatForward = Vector3.ProjectOnPlane(transformCamera.forward, Vector3.up).normalized;
        Vector3 flatRight = Vector3.ProjectOnPlane(transformCamera.right, Vector3.up).normalized;

        if(Input.GetKey(KeyCode.W)) {
            input += flatForward; //Adjust so it's the normalized form .forward
        }
        if(Input.GetKey(KeyCode.S)) {
            input -= flatForward;
        }
        if(Input.GetKey(KeyCode.A)) {
            input -= flatRight;
        }
        if(Input.GetKey(KeyCode.D)) {
            input += flatRight;
        }
        OnMove?.Invoke(input);
    }
}
