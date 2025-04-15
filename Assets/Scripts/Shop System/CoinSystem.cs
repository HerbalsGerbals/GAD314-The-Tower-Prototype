using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public int coinCount;

    public TextMeshProUGUI coinText;

    public void Update()
    {
        coinText.text = "Coin Count: " + coinCount.ToString();
    }
}
