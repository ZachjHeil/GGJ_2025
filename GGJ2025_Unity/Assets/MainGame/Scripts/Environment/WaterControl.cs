using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class WaterControl : MonoBehaviour
{
    [SerializeField] VolumeProfile waterPPE;
    [SerializeField] VolumeProfile surfacePPE;
    [SerializeField] CinemachineVolumeSettings cinemachineCamera;

    private void Awake()
    {
        RenderSettings.fog = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag != "MainCamera") return;

        FindAnyObjectByType<PlayerController>().ChangeWaterEffectState(true);
        cinemachineCamera.Profile = waterPPE;
        RenderSettings.fog = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag != "MainCamera") return;

        FindAnyObjectByType<PlayerController>().ChangeWaterEffectState(false);
        cinemachineCamera.Profile = surfacePPE;
        RenderSettings.fog = true;
    }
}
