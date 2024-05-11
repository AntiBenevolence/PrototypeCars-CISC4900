using UnityEngine;
using UnityEngine.UI;

public class ControllerSettingsManager1 : MonoBehaviour
{
    public GameObject keyboardPanel;
    public GameObject controllerPanel;
    public GameObject steeringWheelPanel;

    public Button keyboardButton;
    public Button controllerButton;
    public Button steeringWheelButton;

    void Start()
    {
        // Set initial panel to keyboard
        ShowKeyboardPanel();
    }

    public void ShowKeyboardPanel()
    {
        keyboardPanel.SetActive(true);
        controllerPanel.SetActive(false);
        steeringWheelPanel.SetActive(false);
    }

    public void ShowControllerPanel()
    {
        keyboardPanel.SetActive(false);
        controllerPanel.SetActive(true);
        steeringWheelPanel.SetActive(false);
    }

    public void ShowSteeringWheelPanel()
    {
        keyboardPanel.SetActive(false);
        controllerPanel.SetActive(false);
        steeringWheelPanel.SetActive(true);
    }
}
