using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject towerPanel;
    [SerializeField] GameObject dungeonPanel;
    public bool cameraChanger = false;
    public bool slimeMovement = false;
    [SerializeField] Canvas CanvasUI;
    public Camera towerCam;
    public Camera dungeonCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchToTowerDefence();
        SlimeMovementStop();
        towerCam.enabled = true;
        dungeonCam.enabled = false;

    }

    public void Update()
    {
        //SwitchView();
    }

    public void SwitchToTowerDefence()
    {
        //Switch To Tower UI
        towerPanel.SetActive(true);
        dungeonPanel.SetActive(false);
        cameraChanger = false;

        //Stop Dungeon Enemy Movement
        SlimeMovementStop();

        //Switch To Tower Camera
        towerCam.enabled = true;
        dungeonCam.enabled = false;
        CanvasUI.worldCamera = towerCam;
    }

    public void SwitchToRoguelike()
    {
        //Switch To Dungeon UI
        towerPanel.SetActive(false);
        dungeonPanel.SetActive(true);
        cameraChanger = true;

        //Start Dungeon Enemy Movement Again
        SlimeMovementStart();

        //Switch To Dungeon Camera
        towerCam.enabled = false;
        dungeonCam.enabled = true;
        CanvasUI.worldCamera = dungeonCam;
    }

    public void SwitchView()
    {
        //Changes Camera Based Of Bool
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
        //When Camera Isn't On Dungeon Screen Stop All Enemy Movement
        GameObject slimeEnemy = GameObject.FindWithTag("Enemy");

        slimeEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        slimeEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SlimeMovementStart()
    {
        //When Camera Is On Dungeon Screen Start Enemy Movement After Second To Allow Player To React
        slimeMovement = true;
        if (slimeMovement == true)
        {
            StartCoroutine(SlimeMovementStartDelay());
        }
    }

    IEnumerator SlimeMovementStartDelay()
    {
        //Delays Enemy's Movement Starting Up By 1 Second 
        Debug.Log("Coroutine Started");
        slimeMovement = false;
        GameObject slimeEnemy = GameObject.FindWithTag("Enemy");
        yield return new WaitForSeconds(1f);
        slimeEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Debug.Log("Coroutine Ended");

    }
}
