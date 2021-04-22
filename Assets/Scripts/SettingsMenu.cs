using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider;
    public Toggle toggle;

    private void Start()
    {
        slider.SetValueWithoutNotify(AudioManager.volume);
        if (!AudioManager.isFullscreen)
        {
            toggle.isOn = false;
        }
    }

    public static void SetVolume(float volume)
    {
        AudioManager.volume = volume;
    }

    public static void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        AudioManager.isFullscreen = isFullscreen;
    }
}
