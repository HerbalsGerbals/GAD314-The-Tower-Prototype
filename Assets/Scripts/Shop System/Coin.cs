using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinSystem coinSystem;

    private void Start()
    {
        coinSystem = FindObjectOfType<CoinSystem>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinSystem.coinCount++;
            Destroy(gameObject);
        }
    }
}
