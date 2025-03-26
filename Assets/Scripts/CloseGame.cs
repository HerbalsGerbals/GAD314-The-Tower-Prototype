using UnityEngine;

public class CloseGame : MonoBehaviour
{
    [SerializeField] private GameObject closeButton;

   public void CloseApplication()
    {
        Application.Quit();
    }
}
