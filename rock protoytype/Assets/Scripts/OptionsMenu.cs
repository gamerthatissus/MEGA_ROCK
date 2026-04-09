using System;
using TMPro;
using Unity.Mathematics;
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

    private GameObject selectedOption;
    private float slideValue = 0f;

    //AudioListener.volume = PlayerPrefs.GetFloat("MasterVol", 1f);


    void OnEnable()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        selectedOption = volumeBar;
    }

    private void Update()
    {
        if (selectedOption == volumeBar)
        {
            ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            masterSlider.value += 5 * slideValue * Time.unscaledDeltaTime;
            AudioListener.volume = masterSlider.value;
            PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
            PlayerPrefs.Save();
        }
    }

    public void MenuUp(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.activeSelf)
        {
            if (selectedOption == volumeBar)
            {
                selectedOption = menuButton;
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else if (selectedOption == FSButton)
            {
                selectedOption = volumeBar;
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedOption = FSButton;
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
    }

    public void MenuDown(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.activeSelf)
        {
            if (selectedOption == FSButton)
            {
                selectedOption = menuButton;
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else if (selectedOption == menuButton)
            {
                selectedOption = volumeBar;
                ExecuteEvents.Execute(menuButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
            else
            {
                selectedOption = FSButton;
                ExecuteEvents.Execute(volumeBar, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                ExecuteEvents.Execute(FSButton, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
            }
        }
    }

    public void PressButton(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.activeSelf)
        {
            if (selectedOption == FSButton)
            {
                fullscreenToggle.isOn = !fullscreenToggle.isOn;
                SetFullscreen(fullscreenToggle.isOn);
            }
            else if (selectedOption == menuButton)
            {
                CloseOptions();
            }
        }
    }

    public void SlideBar(InputAction.CallbackContext context)
    {
        slideValue = context.ReadValue<Vector2>().x;
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
