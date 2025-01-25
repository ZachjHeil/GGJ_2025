using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ToggleGraphicSwap : MonoBehaviour
{

        Toggle _toggle;
        Toggle toggle
        {
            get { return _toggle ?? (_toggle = GetComponent<Toggle>()); }
        }

        void Awake()
        {
            toggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
        }

        void OnEnable()
        {
            toggle.targetGraphic.enabled = !toggle.isOn;
        }

        void OnTargetToggleValueChanged(bool on)
        {
            toggle.targetGraphic.enabled = !on;
        }
}
