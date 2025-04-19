using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReturnButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
