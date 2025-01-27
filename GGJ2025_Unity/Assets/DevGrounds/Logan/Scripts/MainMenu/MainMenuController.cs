using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Canvas startGameCanvas;
    public Canvas settingsCanvas;

    public Button ContinueButton;
    public bool saveDataExists;

    public AudioSource audioSource_SFX;
    public AudioSource audioSource_Music;
    public AudioSource audioSource_Voice;
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

    private void Start()
    {
        if (SavingLoading.Instance != null)
        { 
            if (SavingLoading.Instance.GetIfNewSave())
            { ContinueButton.interactable = false; } 
            else { ContinueButton.interactable = true; }
        }
    }

    public void UpdateFromSettings()
    {
        slider_SFX.value = settings.GetSoundSettings(Enums.SoundOptions.SFX);
        slider_Music.value = settings.GetSoundSettings(Enums.SoundOptions.Music);
        slider_Voice.value = settings.GetSoundSettings(Enums.SoundOptions.Voice);
    }

    public void ToggleStartGame()
    {
        PlayAudio(Enums.SoundOptions.SFX, buttonPress);
        startGameCanvas.enabled = !startGameCanvas.enabled;
        startGameCanvas.GetComponent<CanvasGroup>().enabled = startGameCanvas.enabled;

        if (startGameCanvas.enabled)
        {

        }
    }

    public void NewGame()
    {
        PlayAudio(Enums.SoundOptions.SFX, startGame);
        //Load main level with new data goes here
        SavingLoading.Instance.ResetSaveFile();
        SceneLoader.Instance.ToGameScene();
        
        
    }

    public void ContinueGame()
    {
        PlayAudio(Enums.SoundOptions.SFX, startGame);
        //load main level with saved data goes here
        SceneLoader.Instance.ToGameScene();
    }

    public void ToggleSettingsMenu()
    {
        PlayAudio(Enums.SoundOptions.SFX, buttonPress);
        settingsCanvas.enabled = !settingsCanvas.enabled;
        settingsCanvas.GetComponent<CanvasGroup>().enabled = settingsCanvas.enabled;
    }

    public void QuitGame()
    {
        PlayAudio(Enums.SoundOptions.SFX, stopGame);
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PlayAudio(Enums.SoundOptions audioType, AudioClip clip)
    {
        AudioSource desiredSource;

        switch (audioType)
        {
            case Enums.SoundOptions.SFX:
                desiredSource = audioSource_SFX;
                break;

            case Enums.SoundOptions.Music:
                desiredSource = audioSource_Music;
                break;

            case Enums.SoundOptions.Voice:
                desiredSource = audioSource_Voice;
                break;
            default:
                desiredSource = audioSource_SFX;
                break;

        }

        desiredSource.clip = clip;
        desiredSource.Play();
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
