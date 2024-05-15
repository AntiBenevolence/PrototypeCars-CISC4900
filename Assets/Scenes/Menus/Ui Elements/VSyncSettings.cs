using UnityEngine;
using UnityEngine.UI;

public class VSyncSettings : MonoBehaviour
{
    public Toggle vSyncToggle;

    void Start()
    {
        // Check if V-Sync is enabled and set the toggle accordingly
        vSyncToggle.isOn = QualitySettings.vSyncCount > 0;

        // Add listener for toggle changes
        vSyncToggle.onValueChanged.AddListener(OnVSyncToggleChanged);
    }

    void OnVSyncToggleChanged(bool isVSyncEnabled)
    {
        // Enable or disable V-Sync based on toggle state
        QualitySettings.vSyncCount = isVSyncEnabled ? 1 : 0;
    }
}