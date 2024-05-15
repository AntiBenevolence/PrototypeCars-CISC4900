using UnityEngine;
using UnityEngine.UI;

public class ControllerBindingPanel : MonoBehaviour
{
    public Text accelerateText;
    public Text brakeText;
    public Text steerLeftText;
    public Text steerRightText;
    public Text gearShiftUpText;
    public Text gearShiftDownText;
    public Text changeCameraText;
    public Text handbrakeText;

    public InputField accelerateInputField;
    public InputField brakeInputField;
    public InputField steerLeftInputField;
    public InputField steerRightInputField;
    public InputField gearShiftUpInputField;
    public InputField gearShiftDownInputField;
    public InputField changeCameraInputField;
    public InputField handbrakeInputField;

    void Start()
    {
        // Initialize input fields with current controller bindings
        accelerateInputField.text = PlayerPrefs.GetString("AccelerateButton", "Button 0");
        brakeInputField.text = PlayerPrefs.GetString("BrakeButton", "Button 1");
        steerLeftInputField.text = PlayerPrefs.GetString("SteerLeftButton", "Button 2");
        steerRightInputField.text = PlayerPrefs.GetString("SteerRightButton", "Button 3");
        gearShiftUpInputField.text = PlayerPrefs.GetString("GearShiftUpButton", "Button 4");
        gearShiftDownInputField.text = PlayerPrefs.GetString("GearShiftDownButton", "Button 5");
        changeCameraInputField.text = PlayerPrefs.GetString("ChangeCameraButton", "Button 6");
        handbrakeInputField.text = PlayerPrefs.GetString("HandbrakeButton", "Button 7");
    }

    public void SetAccelerateButton(string button)
    {
        PlayerPrefs.SetString("AccelerateButton", button);
        accelerateInputField.text = button;
    }

    public void SetBrakeButton(string button)
    {
        PlayerPrefs.SetString("BrakeButton", button);
        brakeInputField.text = button;
    }

    public void SetSteerLeftButton(string button)
    {
        PlayerPrefs.SetString("SteerLeftButton", button);
        steerLeftInputField.text = button;
    }

    public void SetSteerRightButton(string button)
    {
        PlayerPrefs.SetString("SteerRightButton", button);
        steerRightInputField.text = button;
    }

    public void SetGearShiftUpButton(string button)
    {
        PlayerPrefs.SetString("GearShiftUpButton", button);
        gearShiftUpInputField.text = button;
    }

    public void SetGearShiftDownButton(string button)
    {
        PlayerPrefs.SetString("GearShiftDownButton", button);
        gearShiftDownInputField.text = button;
    }

    public void SetChangeCameraButton(string button)
    {
        PlayerPrefs.SetString("ChangeCameraButton", button);
        changeCameraInputField.text = button;
    }

    public void SetHandbrakeButton(string button)
    {
        PlayerPrefs.SetString("HandbrakeButton", button);
        handbrakeInputField.text = button;
    }
}
