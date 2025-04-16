using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    public int coinCount;

    public TextMeshProUGUI coinText;

    [SerializeField] private Button arrowTower;
    [SerializeField] private Button bombTower;
    [SerializeField] private Button spikeTower;

    public void Update()
    {
        coinText.text = "Coin Count: " + coinCount.ToString();

        ArrowTowerCoinCount();
        BombTowerCoinCount();
        SpikeTowerCoinCount();

    }

    public void ArrowTowerCoinCount()
    {
        if (coinCount >= 5)
        {
            arrowTower.interactable = true;
  
        }
        else
        {
            arrowTower.interactable = false;
        }
    }

    public void BombTowerCoinCount()
    {
        if (coinCount >= 10)
        {
            bombTower.interactable = true;
        }
        else
        {
            bombTower.interactable = false;
        }
    }

    public void SpikeTowerCoinCount()
    {
        if (coinCount >= 15)
        {
            spikeTower.interactable = true;
        }
        else
        {
            spikeTower.interactable = false;
        }
    }
}
