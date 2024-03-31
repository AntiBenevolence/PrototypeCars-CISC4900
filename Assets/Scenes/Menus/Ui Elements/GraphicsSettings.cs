using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Slider graphicsSlider;
    public Text graphicsQualityText;
    public Slider brightnessSlider; // Reference to the brightness slider UI element
    public Text brightnessValueText; // Optional: Text element to display the current brightness value

    // Define the graphics quality levels
    private string[] qualityLevels = { "Low", "Medium", "High", "Ultra" };

    void Start()
    {
        // Set the slider's max value to the highest index in the qualityLevels array
        graphicsSlider.maxValue = qualityLevels.Length - 1;

        // Initialize the slider value and graphics quality text
        graphicsSlider.value = QualitySettings.GetQualityLevel();
        UpdateGraphicsQualityText();

        // Add a listener to the slider for value changes
        graphicsSlider.onValueChanged.AddListener(OnGraphicsSliderChanged);

        // Add listener for brightness slider changes
        brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

        // Optional: Initialize brightness value text
        UpdateBrightnessValueText();
    }

    void OnGraphicsSliderChanged(float value)
    {
        // Round the slider value to an integer for quality level index
        int qualityLevelIndex = Mathf.RoundToInt(value);

        // Clamp the index to ensure it's within the valid range
        qualityLevelIndex = Mathf.Clamp(qualityLevelIndex, 0, qualityLevels.Length - 1);

        // Set the graphics quality level based on the index
        QualitySettings.SetQualityLevel(qualityLevelIndex);

        // Update the graphics quality text
        UpdateGraphicsQualityText();
    }

    void UpdateGraphicsQualityText()
    {
        // Get the current quality level index
        int currentQualityLevelIndex = QualitySettings.GetQualityLevel();

        // Ensure the index is within the valid range
        currentQualityLevelIndex = Mathf.Clamp(currentQualityLevelIndex, 0, qualityLevels.Length - 1);

        // Update the UI Text component directly with the quality level text
        graphicsQualityText.text = "Graphics Quality: " + qualityLevels[currentQualityLevelIndex];
    }

    public void OnBrightnessChanged(float brightness)
    {
        // Adjust environmental lighting based on the slider value
        RenderSettings.skybox.SetFloat("_Exposure", brightness);

        // Optional: Update brightness value text
        UpdateBrightnessValueText();
    }

    void UpdateBrightnessValueText()
    {
        // Optional: Update the brightness value text to display the current slider value
        if (brightnessValueText != null)
        {
            brightnessValueText.text = brightnessSlider.value.ToString("F2");
        }
    }
}
