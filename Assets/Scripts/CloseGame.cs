using UnityEngine;

public class CloseGame : MonoBehaviour
{
    // [SerializeField] private GameObject closeButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseApplication();
        }
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
}
