using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{  
    [SerializeField] private float rotationSpeed = 50f;

    public UnityEvent OnCollectCoin = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            OnCollectCoin?.Invoke();

            Destroy(gameObject);
        }
    }
}
