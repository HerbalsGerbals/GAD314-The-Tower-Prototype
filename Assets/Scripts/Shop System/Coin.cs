using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinSystem coinSystem;

    private void Start()
    {
        coinSystem = Object.FindAnyObjectByType<CoinSystem>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coinSystem.coinCount += 1;
            Destroy(gameObject);
        }
    }
}
