using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("MasterVol", 1f); // Saves volume set


    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("Titlescreen");
    }
}