using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float pHealth;
    public float pMaxHealth = 20;
    [SerializeField] private Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = pMaxHealth;
        healthSlider.value = pHealth;

        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        pHealth -= damage;
        healthSlider.value = pHealth;

        if (pHealth <= 0)
        {
            //Death Logic currently resets scene
            SceneManager.LoadScene("Game Over");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Player Takes Damage
        if (collision.gameObject.CompareTag("Enemy"))
            TakeDamage(1);
        Debug.Log("Player Hit");
    }
}

