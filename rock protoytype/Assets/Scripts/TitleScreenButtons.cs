using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenButtons : MonoBehaviour
{
    public void TitleStartButton()
    {
        SceneManager.LoadScene("MainMenu");



    }
    public void TitleQuitButton()
    {
        Application.Quit();
    }
}
