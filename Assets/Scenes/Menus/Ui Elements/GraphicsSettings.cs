using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Slider graphicsSlider;
    public Text graphicsQualityText;

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
}