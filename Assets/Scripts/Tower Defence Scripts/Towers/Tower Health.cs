using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public float currentHealth;
    public float towerHealth = 100f;
    public Slider towerHealthSlider;

    private void Start()
    {
        currentHealth = towerHealth;
        towerHealthSlider.maxValue = towerHealth;
        towerHealthSlider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        towerHealthSlider.value = currentHealth;

        if (currentHealth <= 0f)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
