using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    /// <summary>
    /// Starts the first scene of the game.
    /// </summary>
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Loads the options menu.
    /// </summary>
    public void OptionsButton()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    /// <summary>
    /// Loads the credits menu.
    /// </summary>
    public void CreditsButton()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
