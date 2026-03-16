using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class TitleScreenButtons : MonoBehaviour
{
    public GameObject startButton;
    public GameObject quitButton;

    private Action selectedButton;

    private void Start()
    {
        selectedButton = TitleStartButton;
        ExecuteEvents.Execute(startButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    public void MenuScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (selectedButton == TitleStartButton)
            {
                selectedButton = TitleQuitButton;
                ExecuteEvents.Execute(startButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(quitButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedButton = TitleStartButton;
                ExecuteEvents.Execute(quitButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(startButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
    }

    public void PressButton(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            selectedButton.Invoke();
        }
    }

    public void TitleStartButton()
    {
        SceneManager.LoadScene("MainMenu");



    }
    public void TitleQuitButton()
    {
        Application.Quit();
    }
}
