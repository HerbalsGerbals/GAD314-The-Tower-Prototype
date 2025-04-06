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

    void Start()
    {
        // Find the TowerPlacer script on the same object 
        towerPlacerScript = FindObjectOfType<TowerPlacer>();

        // UI buttons to select towers
        foreach (Button button in towerButtons)
        {
            button.onClick.AddListener(() => OnTowerButtonClicked(button));
        }
    }

    void OnTowerButtonClicked(Button button)
    {
        // Get the index of the selected tower 
        int towerIndex = Array.IndexOf(towerButtons, button); 
        if (towerIndex >= 0 && towerIndex < towerPrefabs.Length)
        {
            // Toggle the placement mode for the selected tower
            isPlacingTower = !isPlacingTower;


            // Set the current tower prefab on the TowerPlacer script
            towerPlacerScript.SetCurrentTower(towerIndex, isPlacingTower);
        }
    }

}
