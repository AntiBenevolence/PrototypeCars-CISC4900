using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    public Text rpmText;
    public Text gearText;
    private FRMuscleController carController;

    void Start()
    {
        // Find the car controller script on the selected car
        GameObject selectedCar = GameObject.FindWithTag("Player");
        if (selectedCar != null)
        {
            carController = selectedCar.GetComponent<FRMuscleController>();
            if (carController != null)
            {
                Debug.Log("Car controller found and assigned.");
            }
            else
            {
                Debug.LogError("FRMuscleController script not found on the selected car.");
            }
        }
        else
        {
            Debug.LogError("No car with the tag 'Player' found in the scene.");
        }
    }

    void Update()
    {
        if (carController != null)
        {
            // Update the RPM and gear UI elements
            rpmText.text = "RPM: " + Mathf.Round(carController.rpm).ToString();
            gearText.text = "Gear: " + GetGearText(carController.currentGear);
            Debug.Log("RPM: " + carController.rpm + " Gear: " + carController.currentGear);
        }
        else
        {
            Debug.LogError("Car controller is null.");
        }
    }

    private string GetGearText(int gear)
    {
        if (gear == 0)
        {
            return "N"; // Neutral
        }
        return gear.ToString();
    }
}
