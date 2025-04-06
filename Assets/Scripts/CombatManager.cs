using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private BallonEnemey enemy;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            Debug.Log("Player Hit");
        }
    }

}
