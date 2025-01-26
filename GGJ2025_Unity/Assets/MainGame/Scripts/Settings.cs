using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    public AudioMixer AudioMix;
    public int SFX_Volume;
    public int Music_Volume;
    public int Voice_Volume;
    public Enums.ControlScheme controlScheme;

    public void LoadFromFile()
    {
        




    }

    public void UpdateMixers()
    {
        AudioMix.SetFloat("SFXVol", SFX_Volume);
        AudioMix.SetFloat("MusicVol", Music_Volume);
        AudioMix.SetFloat("VoiceVol", Voice_Volume);
    }

    public int GetSoundSettings(Enums.SoundOptions option)
    {
        switch (option)
        {
            case Enums.SoundOptions.SFX:
                return SFX_Volume;
            case Enums.SoundOptions.Music:
                return Music_Volume;
            case Enums.SoundOptions.Voice:
                return Voice_Volume;
            default:
                return 0;
        }
    }
}
