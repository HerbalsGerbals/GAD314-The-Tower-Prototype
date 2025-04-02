using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera towerCam;
    public Camera dungeonCam;

    public void Start()
    {
        towerCam.enabled = true;
        dungeonCam.enabled = false;
    }
    public void ShowTowerDefence()
    {
        towerCam.enabled = true;
        dungeonCam.enabled = false;
    }

    public void ShowDungeon()
    {
        towerCam.enabled = false;
        dungeonCam.enabled = true;
    }
   
}
