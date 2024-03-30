using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FullscreenSettings : MonoBehaviour
{
    public Dropdown fullscreenDropdown;

    void Start()
    {
        // Set the dropdown options programmatically
        List<string> options = new List<string>
        {
            "Windowed",
            "Fullscreen"
        };

        fullscreenDropdown.ClearOptions();
        fullscreenDropdown.AddOptions(options);

        // Initialize dropdown value based on current fullscreen mode
        int currentMode = Screen.fullScreen ? 1 : 0;
        fullscreenDropdown.value = currentMode;

        // Add listener for dropdown changes
        fullscreenDropdown.onValueChanged.AddListener(OnFullscreenDropdownChanged);
    }

    void OnFullscreenDropdownChanged(int mode)
    {
        switch (mode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1:
                Screen.fullScreen = true;
                break;
            default:
                Debug.LogWarning("Invalid fullscreen mode selected.");
                break;
        }
    }
}