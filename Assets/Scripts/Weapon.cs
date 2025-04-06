using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1;
    public enum WeaponType {Melee, Bullet}
    public WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //BallonEnemey Takes Damage
        if (other.tag == "Enemy")
        {
            other.GetComponent<BallonEnemey>().TakeDamage(damage);
            Debug.Log("Enemy Hit");
        }

    }


}
