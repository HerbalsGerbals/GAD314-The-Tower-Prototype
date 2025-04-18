using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject closeButton;
    public void PlayGameButton()
    {
        SceneManager.LoadScene("Current Prototype");
    }

    public void InstructionsButton()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }

    public void CloseButton()
    {
        {
            mainMenu.SetActive(true);
            instructions.SetActive(false);
        }
    }
}
