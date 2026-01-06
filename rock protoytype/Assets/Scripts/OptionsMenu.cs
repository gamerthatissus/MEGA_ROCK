using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Toggle fullscreenToggle;

    //AudioListener.volume = PlayerPrefs.GetFloat("MasterVol", 1f);


    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVol", 1f);
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
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
