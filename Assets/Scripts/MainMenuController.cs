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
    [SerializeField] GameObject howToPlayMenu;

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
        howToPlayMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void HowToPlay()
    {
        mainMenu.SetActive(false);
        howToPlayMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
