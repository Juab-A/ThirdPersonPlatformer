using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float score = 0;

    private Coin[] coins;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = $"Score: {score}";

        coins = FindObjectsByType<Coin>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Coin coin in coins) {
            coin.OnCollectCoin.AddListener(IncrementScore);
        }
    }

    private void IncrementScore() {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
