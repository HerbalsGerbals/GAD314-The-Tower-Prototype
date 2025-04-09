using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Attack : MonoBehaviour
{
    //Melee
    public GameObject melee;
    public Animator meleeAttack;
    bool isAttacking = false;
    float atkDuration = 0.3f;
    float atkTimer = 0f;

    //Ranged
    public Transform aim;
    public GameObject bullet;
    public float fireForce = 10f;
    float shootCooldown = 0.25f;
    float shootTimer = 0.5f;
    float attackCounter = 0.5f;
    float attackTime;

    public UIManager UIManager;


    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
        shootTimer += Time.deltaTime;
        attackCounter -= Time.deltaTime;
        if (UIManager.cameraChanger == true)
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnAttack();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShoot();
            }
        }
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
            melee.SetActive(true);
            meleeAttack.SetTrigger("Attack");
            isAttacking = true;
            Debug.Log("Attack");
        }
    }

    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            atkTimer += Time.deltaTime;
            if (atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                melee.SetActive(false);
                Debug.Log("Attack over");
            }
        }
    }

    void OnShoot()
    {
        if (shootTimer > shootCooldown)
        {
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, aim.position, aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(-aim.up * fireForce, ForceMode2D.Impulse);
            intBullet.transform.position = aim.position;
            Destroy(intBullet, 2f);
        }
    }
}
