using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] carModels; // Array of car models
    public Transform[] spawnPoints; // Array of spawn points
    private GameObject currentCar; // Currently displayed car
    public float rotationSpeed = 10f; // Speed of car rotation

    private int currentIndex = 0; // Index of the currently selected car
    public Button leftButton; // Left button to navigate to previous car
    public Button rightButton; // Right button to navigate to next car
    public Button selectButton; // Select button to choose the car
    public Scrollbar rotationScrollbar; // UI scrollbar for car rotation

    void Start()
    {
        leftButton.onClick.AddListener(PreviousCar);
        rightButton.onClick.AddListener(NextCar);
        selectButton.onClick.AddListener(SelectCar);

        // Initialize the selected car
        LoadCar(currentIndex);

        // Set initial rotation
        rotationScrollbar.value = 0f;
        RotateCar();
    }

    void Update()
    {
        RotateCar();
    }

    void RotateCar()
    {
        // Rotate the selected car based on the scrollbar value
        float scrollbarValue = rotationScrollbar.value;
        float rotationAmount = scrollbarValue * 360f; // Convert scrollbar value to rotation angle
        if (currentCar != null)
        {
            currentCar.transform.localRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        }
    }

    void PreviousCar()
    {
        // Update the index to navigate to the previous car
        currentIndex = (currentIndex - 1 + carModels.Length) % carModels.Length;
        LoadCar(currentIndex);
    }

    void NextCar()
    {
        // Update the index to navigate to the next car
        currentIndex = (currentIndex + 1) % carModels.Length;
        LoadCar(currentIndex);
    }

    void LoadCar(int index)
    {
        if (currentCar != null)
        {
            Destroy(currentCar);
        }

        Debug.Log($"Loading car at index: {index}");

        if (carModels[index] != null)
        {
            Transform spawnPoint = spawnPoints[index];
            Debug.Log($"Spawn point position: {spawnPoint.position}, rotation: {spawnPoint.rotation.eulerAngles}");
            currentCar = Instantiate(carModels[index], spawnPoint.position, spawnPoint.rotation);
            currentCar.transform.SetParent(spawnPoint, false); // Maintain the local position and rotation

            Debug.Log($"Successfully loaded car: {currentCar.name}");

            // Adjust the local position and rotation to ensure it matches the spawn point
            currentCar.transform.localPosition = Vector3.zero;
            currentCar.transform.localRotation = Quaternion.identity;

            // Additional debug to check car position and scale
            Debug.Log($"Car position: {currentCar.transform.position}, Car rotation: {currentCar.transform.rotation.eulerAngles}, Car scale: {currentCar.transform.localScale}");

            // Make sure the car is active
            currentCar.SetActive(true);

            // Additional check for Renderer component
            Renderer carRenderer = currentCar.GetComponent<Renderer>();
            if (carRenderer != null)
            {
                Debug.Log("Car Renderer found.");
            }
            else
            {
                Debug.LogError("Car Renderer not found.");
            }
        }
        else
        {
            Debug.LogError($"Car model at index {index} is null!");
        }
    }

    void SelectCar()
    {
        // Implement the functionality to select the chosen car
        Debug.Log("Selected car: " + carModels[currentIndex].name);

        // Load the "cityscene" scene
        SceneManager.LoadScene("cityscene");

        // Set up a way to pass the selected car's index to the next scene
        PlayerPrefs.SetInt("SelectedCarIndex", currentIndex);
    }
}
