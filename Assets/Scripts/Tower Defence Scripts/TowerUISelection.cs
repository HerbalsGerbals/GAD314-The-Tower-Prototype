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
        towerPlacerScript = FindAnyObjectByType<TowerPlacer>();

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

            //if (isPlacingTower)
            //{
            //    // Create the ghost tower when starting placement
            //    //CreateGhostTower(towerIndex);
            //}
            //else
            //{
            //    // Destroy the ghost tower when exiting placement mode
            //    DestroyGhostTower();
            //}

            // Set the current tower prefab on the TowerPlacer script
            towerPlacerScript.SetCurrentTower(towerIndex, isPlacingTower);
        }
    }

    //void CreateGhostTower(int towerIndex)
    //{
    //    // Create a ghost tower object and make it semi-transparent
    //    ghostTower = Instantiate(towerPrefabs[towerIndex]);
    //    Renderer renderer = ghostTower.GetComponent<Renderer>();
    //    if (renderer != null)
    //    {
    //        Color color = renderer.material.color;
    //        color.a = 0.5f; // Set transparency to 50%
    //        renderer.material.color = color;
    //    }

    //    ghostTower.SetActive(true);
    //}

    //void DestroyGhostTower()
    //{
    //    if (ghostTower != null)
    //    {
    //        Destroy(ghostTower);
    //    }
    //}
}
