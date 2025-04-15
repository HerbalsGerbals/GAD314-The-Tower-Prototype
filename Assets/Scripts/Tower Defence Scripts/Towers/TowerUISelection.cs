using UnityEngine;
using UnityEngine.UI;
using System;

public class TowerUISelection : MonoBehaviour
{
    public Button[] towerButtons; 
    public GameObject[] towerPrefabs; 
    private TowerPlacer towerPlacerScript; 

    public bool isPlacingTower = false; 
    private GameObject ghostTower; // The ghost tower object

    public CoinSystem coinSystem;

    public void Start()
    {
        towerPlacerScript = FindFirstObjectByType<TowerPlacer>();
        coinSystem = FindFirstObjectByType<CoinSystem>();

        for (int i = 0; i < towerButtons.Length; i++)
        {
            int index = i; // Capture index for the lambda
            towerButtons[i].onClick.AddListener(() => OnTowerButtonClicked(index));
        }
    }

    public void OnTowerButtonClicked(int towerIndex)
    {
        GameObject towerPrefab = towerPrefabs[towerIndex];
        Tower towerData = towerPrefab.GetComponent<Tower>();

        if (towerData != null && coinSystem.coinCount >= towerData.cost)
        {
            isPlacingTower = true;
            towerPlacerScript.SetCurrentTower(towerIndex, isPlacingTower);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    public void DeductCoins(int amount)
    {
        coinSystem.coinCount -= amount;
    }

}
