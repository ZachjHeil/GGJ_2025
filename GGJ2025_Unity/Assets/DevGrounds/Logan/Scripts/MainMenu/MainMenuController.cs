using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MainMenuController : MonoBehaviour
{
    public Canvas startGameCanvas;
    public Canvas settingsCanvas;

    public Button ContinueButton;
    public bool saveDataExists;

    public AudioSource audioSource;
    public AudioClip startGame;
    public AudioClip buttonPress;
    public AudioClip stopGame;

    public List<ToggleC> controlOptions;

    public Settings settings;
    public Slider slider_SFX;
    public Slider slider_Music;
    public Slider slider_Voice;

    private void Awake()
    {
        settings.LoadFromFile();

        UpdateFromSettings();
    }

    public void UpdateFromSettings()
    {
        slider_SFX.value = settings.GetSoundSettings(Enums.SoundOptions.SFX);
        slider_Music.value = settings.GetSoundSettings(Enums.SoundOptions.Music);
        slider_Voice.value = settings.GetSoundSettings(Enums.SoundOptions.Voice);
    }

    public void ToggleStartGame()
    {
        PlayAudio(buttonPress);
        startGameCanvas.enabled = !startGameCanvas.enabled;
        startGameCanvas.GetComponent<CanvasGroup>().enabled = startGameCanvas.enabled;

        if (startGameCanvas.enabled)
        {
            ContinueButton.interactable = saveDataExists;
        }
    }

    public void NewGame()
    {
        PlayAudio(startGame);
        //Load main level with new data goes here
    }

    public void ContinueGame()
    {
        PlayAudio(startGame);
        //load main level with saved data goes here
    }

    public void ToggleSettingsMenu()
    {
        PlayAudio(buttonPress);
        settingsCanvas.enabled = !settingsCanvas.enabled;
        settingsCanvas.GetComponent<CanvasGroup>().enabled = settingsCanvas.enabled;
    }

    public void QuitGame()
    {
        PlayAudio(stopGame);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ChangeControls(int scheme)
    {
        settings.controlScheme = (Enums.ControlScheme)scheme;

        switch (settings.controlScheme)
        {
            case Enums.ControlScheme.Righty:
                controlOptions[0].isOn = true;
                break;

            case Enums.ControlScheme.Lefty:
                controlOptions[1].isOn = true;
                break;

            case Enums.ControlScheme.Xbox:
                controlOptions[2].isOn = true;
                break;

            case Enums.ControlScheme.Playstation:
                controlOptions[3].isOn = true;
                break;

            default:
                controlOptions[0].isOn = true;
                break;
        }
    }

    public void AdjustSFX(float val)
    {
        settings.SFX_Volume = (int)val;
        settings.UpdateMixers();
    }
    public void AdjustMusic(float val)
    {
        settings.Music_Volume = (int)val;
        settings.UpdateMixers();
    }
    public void AdjustVoice(float val)
    {
        settings.Voice_Volume = (int)val;
        settings.UpdateMixers();
    }

}
