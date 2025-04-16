using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    public float tHealth;
    [SerializeField] private Slider towerSlider;

    // Start is called before the first frame update
    void Start()
    {
        tHealth = 10;
        towerSlider.value = tHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        tHealth -= damage;

        if (tHealth <= 0)
        {
            //Death Logic currently resets scene
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateUI()
    {
        towerSlider.value = tHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Player Takes Damage
        if (collision.gameObject.CompareTag("BalloonEnemy"))
            TakeDamage(1);
        Debug.Log("Village Took Damage");
    }
}
