using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject towerPanel;
    [SerializeField] GameObject dungeonPanel;
    public CameraControl cameraControl;
    public bool cameraChanger = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchToTowerDefence();
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
    }

    public void SwitchToRoguelike()
    {
        towerPanel.SetActive(false);
        dungeonPanel.SetActive(true);
        cameraControl.ShowDungeon();
        cameraChanger = true;
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
}
