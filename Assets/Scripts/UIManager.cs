using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject towerPanel;
    [SerializeField] GameObject dungeonPanel;
    public CameraControl cameraControl;
    public bool cameraChanger = false;
    public bool slimeMovement = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchToTowerDefence();
        SlimeMovementStop();
    }

    public void Update()
    {
        //SwitchView();
    }

    public void SwitchToTowerDefence()
    {
        towerPanel.SetActive(true);
        dungeonPanel.SetActive(false);
        cameraControl.ShowTowerDefence();
        cameraChanger = false;
        SlimeMovementStop();
    }

    public void SwitchToRoguelike()
    {
        towerPanel.SetActive(false);
        dungeonPanel.SetActive(true);
        cameraControl.ShowDungeon();
        cameraChanger = true;
        SlimeMovementStart();
    }

    public void SwitchView()
    {
        if (cameraChanger == false)
        {
            SwitchToRoguelike();
        }
        else if (cameraChanger == true)
        {
            SwitchToTowerDefence();
        }


    }

    public void SlimeMovementStop()
    {
        GameObject slimeEnemy = GameObject.FindWithTag("Enemy");

        slimeEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void SlimeMovementStart()
    {
        slimeMovement = true;
        if (slimeMovement == true)
        {
            StartCoroutine(SlimeMovementStartDelay());
        }
    }

    IEnumerator SlimeMovementStartDelay()
    {
        Debug.Log("Coroutine Started");
        slimeMovement = false;
        GameObject slimeEnemy = GameObject.FindWithTag("Enemy");
        yield return new WaitForSeconds(1f);
        slimeEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;   
        Debug.Log("Coroutine Ended");
        
    }
}
