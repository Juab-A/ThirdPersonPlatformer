using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSpacePressed = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {

        }

        if(Input.GetKeyDown(KeyCode.W)) {

        }
        if(Input.GetKeyDown(KeyCode.S)) {
            
        }
        if(Input.GetKeyDown(KeyCode.A)) {
            
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            
        }
    }
}
