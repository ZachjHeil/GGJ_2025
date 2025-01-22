using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class WaterControl : MonoBehaviour
{
    [SerializeField] VolumeProfile waterPPE;
    [SerializeField] VolumeProfile surfacePPE;
    [SerializeField] CinemachineVolumeSettings cinemachineCamera;

    private void Awake()
    {
        RenderSettings.fog = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag != "MainCamera") return;

        cinemachineCamera.Profile = waterPPE;
        RenderSettings.fog = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag != "MainCamera") return;

        cinemachineCamera.Profile = surfacePPE;
        RenderSettings.fog = false;
    }
}
