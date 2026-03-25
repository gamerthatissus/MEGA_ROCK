using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionsPanel;

    public GameObject playButton;
    public GameObject titleButton;
    public GameObject optionsButton;

    private Action selectedButton;

    private void OnEnable()
    {
        selectedButton = PlayGame;
        ExecuteEvents.Execute(playButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    public void MenuUp(InputAction.CallbackContext context)
    {
        if (context.performed && !optionsPanel.activeSelf && gameObject.activeSelf)
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
        if (context.performed && !optionsPanel.activeSelf && gameObject.activeSelf)
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
        if (context.performed && !optionsPanel.activeSelf && gameObject.activeSelf)
        {
            selectedButton.Invoke();
        }
    }

    public void PlayGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }
    public void BackToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}