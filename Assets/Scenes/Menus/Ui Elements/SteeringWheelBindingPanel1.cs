using UnityEngine;
using UnityEngine.UI;

public class SteeringWheelBindingPanel1 : MonoBehaviour
{
    public Text accelerateText;
    public Text brakeText;
    public Text steerText;
    public Text handbrakeText;
    public Text gearShiftUpText;
    public Text gearShiftDownText;
    public Text changeCameraText;

    public InputField accelerateInputField;
    public InputField brakeInputField;
    public InputField steerInputField;
    public InputField handbrakeInputField;
    public InputField gearShiftUpInputField;
    public InputField gearShiftDownInputField;
    public InputField changeCameraInputField;

    void Start()
    {
        // Initialize input fields with current steering wheel bindings
        accelerateInputField.text = PlayerPrefs.GetString("AcceleratePedal", "Accelerate Pedal");
        brakeInputField.text = PlayerPrefs.GetString("BrakePedal", "Brake Pedal");
        steerInputField.text = PlayerPrefs.GetString("SteeringWheel", "Steering Wheel");
        handbrakeInputField.text = PlayerPrefs.GetString("HandbrakeButton", "Handbrake Button");
        gearShiftUpInputField.text = PlayerPrefs.GetString("GearShiftUpButton", "Gear Shift Up Button");
        gearShiftDownInputField.text = PlayerPrefs.GetString("GearShiftDownButton", "Gear Shift Down Button");
        changeCameraInputField.text = PlayerPrefs.GetString("ChangeCameraButton", "Change Camera Button");
    }

    public void SetAcceleratePedal(string pedal)
    {
        PlayerPrefs.SetString("AcceleratePedal", pedal);
        accelerateInputField.text = pedal;
    }

    public void SetBrakePedal(string pedal)
    {
        PlayerPrefs.SetString("BrakePedal", pedal);
        brakeInputField.text = pedal;
    }

    public void SetSteeringWheel(string wheel)
    {
        PlayerPrefs.SetString("SteeringWheel", wheel);
        steerInputField.text = wheel;
    }

    public void SetHandbrakeButton(string button)
    {
        PlayerPrefs.SetString("HandbrakeButton", button);
        handbrakeInputField.text = button;
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

    public void ResetBindings()
    {
        PlayerPrefs.DeleteKey("AcceleratePedal");
        PlayerPrefs.DeleteKey("BrakePedal");
        PlayerPrefs.DeleteKey("SteeringWheel");
        PlayerPrefs.DeleteKey("HandbrakeButton");
        PlayerPrefs.DeleteKey("GearShiftUpButton");
        PlayerPrefs.DeleteKey("GearShiftDownButton");
        PlayerPrefs.DeleteKey("ChangeCameraButton");

        // Reset input fields to default values
        accelerateInputField.text = "Accelerate Pedal";
        brakeInputField.text = "Brake Pedal";
        steerInputField.text = "Steering Wheel";
        handbrakeInputField.text = "Handbrake Button";
        gearShiftUpInputField.text = "Gear Shift Up Button";
        gearShiftDownInputField.text = "Gear Shift Down Button";
        changeCameraInputField.text = "Change Camera Button";
    }
}
