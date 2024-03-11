using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject GeneralCanvas;
    public GameObject VideoCanvas;
    public GameObject AudioCanvas;
    public GameObject ControlCanvas;
    public GameObject MainMenuButtons;

    void Start()
    {
        // By default, show the graphics settings canvas and hide the others
        MainMenuButtons.SetActive(false);
        GeneralCanvas.SetActive(true);
        VideoCanvas.SetActive(false);
        AudioCanvas.SetActive(false);
        ControlCanvas.SetActive(false);
    }

    public void ShowVideoSettings()
    {
        MainMenuButtons.SetActive(false);
        GeneralCanvas.SetActive(false);
        VideoCanvas.SetActive(true);
        AudioCanvas.SetActive(false);
        ControlCanvas.SetActive(false);
    }

    public void ShowAudioSettings()
    {
        MainMenuButtons.SetActive(false);
        GeneralCanvas.SetActive(false);
        VideoCanvas.SetActive(false);
        AudioCanvas.SetActive(true);
        ControlCanvas.SetActive(false);
    }

    public void ShowControlSettings()
    {
        MainMenuButtons.SetActive(false);
        GeneralCanvas.SetActive(false);
        VideoCanvas.SetActive(false);
        AudioCanvas.SetActive(false);
        ControlCanvas.SetActive(true);
    }

    public void ShowGeneralSettings()
    {
        MainMenuButtons.SetActive(false);
        GeneralCanvas.SetActive(true);
        VideoCanvas.SetActive(false);
        AudioCanvas.SetActive(false);
        ControlCanvas.SetActive(false);
    }

    public void AcceptSettings()
    {
        MainMenuButtons.SetActive(true); 
        GeneralCanvas.SetActive(false);
        VideoCanvas.SetActive(false);
        AudioCanvas.SetActive(false);
        ControlCanvas.SetActive(false);
    }
    
}