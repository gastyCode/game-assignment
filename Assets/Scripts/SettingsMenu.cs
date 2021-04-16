using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.SetValueWithoutNotify(AudioManager.volume);
    }

    public static void SetVolume(float volume)
    {
        AudioManager.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
