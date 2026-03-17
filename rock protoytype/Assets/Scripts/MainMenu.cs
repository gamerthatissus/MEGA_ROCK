using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public GameObject playButton;
    public GameObject titleButton;
    public GameObject optionsButton;

    private Action selectedButton;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("MasterVol", 1f); // Saves volume set
        selectedButton = PlayGame;
        ExecuteEvents.Execute(playButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    public void MenuUp(InputAction.CallbackContext context)
    {
        if (context.performed && !optionsPanel.activeSelf)
        {
            if (selectedButton == PlayGame)
            {
                selectedButton = BackToTitle;
                ExecuteEvents.Execute(playButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(titleButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else if (selectedButton == OpenOptions)
            {
                selectedButton = PlayGame;
                ExecuteEvents.Execute(optionsButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(playButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedButton = OpenOptions;
                ExecuteEvents.Execute(titleButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(optionsButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        if (context.performed && !optionsPanel.activeSelf)
        {
            if (selectedButton == OpenOptions)
            {
                selectedButton = BackToTitle;
                ExecuteEvents.Execute(optionsButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(titleButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else if (selectedButton == BackToTitle)
            {
                selectedButton = PlayGame;
                ExecuteEvents.Execute(titleButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(playButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedButton = OpenOptions;
                ExecuteEvents.Execute(playButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(optionsButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
    }

    public void PressButton(InputAction.CallbackContext context)
    {
        if (context.performed && !optionsPanel.activeSelf)
        {
            selectedButton.Invoke();
        }
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