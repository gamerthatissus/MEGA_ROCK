using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Toggle fullscreenToggle;

    public GameObject volumeBar;
    public GameObject FSButton;
    public GameObject menuButton;

    private Action selectedButton;

    //AudioListener.volume = PlayerPrefs.GetFloat("MasterVol", 1f);


    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        selectedButton = SetMasterVolume;
        ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
    }

    public void MenuUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (selectedButton == SetMasterVolume)
            {
                selectedButton = CloseOptions;
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else if (selectedButton == SetFullscreen)
            {
                selectedButton = SetMasterVolume;
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedButton = SetFullscreen;
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (selectedButton == SetFullscreen)
            {
                selectedButton = CloseOptions;
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else if (selectedButton == CloseOptions)
            {
                selectedButton = SetMasterVolume;
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedButton = SetFullscreen;
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
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

    public void SetMasterVolume(float vol)
    {
        AudioListener.volume = vol;
        PlayerPrefs.SetFloat("MasterVol", vol);
        PlayerPrefs.Save();
    }
    public void SetFullscreen(bool isFS)
    {
        Screen.fullScreen = isFS;
        PlayerPrefs.SetInt("Fullscreen", isFS ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void CloseOptions()
    {
        gameObject.SetActive(false); // Hides this entire panel  
    }
}
