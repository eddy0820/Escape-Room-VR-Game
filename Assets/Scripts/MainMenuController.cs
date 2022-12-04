using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Scene]
    [SerializeField] string firstLevel;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    public void CreditsMenu()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void MainMenu()
    {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
