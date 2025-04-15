using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float pHealth;
    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = 10;
        healthSlider.value = pHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void TakeDamage(float damage)
    {
        pHealth -= damage;

        if (pHealth <= 0)
        {
            //Death Logic currently resets scene
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateUI()
    {
        healthSlider.value = pHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Player Takes Damage
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(1);
        Debug.Log("Player Hit");
    }
}

