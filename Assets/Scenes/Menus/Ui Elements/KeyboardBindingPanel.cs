using UnityEngine;
using UnityEngine.UI;

public class KeyboardBindingPanel : MonoBehaviour
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
        // Initialize input fields with current key bindings
        accelerateInputField.text = PlayerPrefs.GetString("AccelerateKey", "W");
        brakeInputField.text = PlayerPrefs.GetString("BrakeKey", "S");
        steerLeftInputField.text = PlayerPrefs.GetString("SteerLeftKey", "A");
        steerRightInputField.text = PlayerPrefs.GetString("SteerRightKey", "D");
        gearShiftUpInputField.text = PlayerPrefs.GetString("GearShiftUpKey", "E");
        gearShiftDownInputField.text = PlayerPrefs.GetString("GearShiftDownKey", "Q");
        changeCameraInputField.text = PlayerPrefs.GetString("ChangeCameraKey", "C");
        handbrakeInputField.text = PlayerPrefs.GetString("HandbrakeKey", "Space");
    }

    public void SetAccelerateKey(string key)
    {
        PlayerPrefs.SetString("AccelerateKey", key);
        accelerateInputField.text = key;
    }

    public void SetBrakeKey(string key)
    {
        PlayerPrefs.SetString("BrakeKey", key);
        brakeInputField.text = key;
    }

    public void SetSteerLeftKey(string key)
    {
        PlayerPrefs.SetString("SteerLeftKey", key);
        steerLeftInputField.text = key;
    }

    public void SetSteerRightKey(string key)
    {
        PlayerPrefs.SetString("SteerRightKey", key);
        steerRightInputField.text = key;
    }

    public void SetGearShiftUpKey(string key)
    {
        PlayerPrefs.SetString("GearShiftUpKey", key);
        gearShiftUpInputField.text = key;
    }

    public void SetGearShiftDownKey(string key)
    {
        PlayerPrefs.SetString("GearShiftDownKey", key);
        gearShiftDownInputField.text = key;
    }

    public void SetChangeCameraKey(string key)
    {
        PlayerPrefs.SetString("ChangeCameraKey", key);
        changeCameraInputField.text = key;
    }

    public void SetHandbrakeKey(string key)
    {
        PlayerPrefs.SetString("HandbrakeKey", key);
        handbrakeInputField.text = key;
    }
}
