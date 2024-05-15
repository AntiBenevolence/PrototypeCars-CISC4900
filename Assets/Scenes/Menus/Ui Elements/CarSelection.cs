using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    public GameObject[] carModels; // Array of car models
    private GameObject currentCar; // Currently displayed car
    public Transform spawnPoint; // Point to spawn the car
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
        currentCar = Instantiate(carModels[currentIndex], spawnPoint.position, Quaternion.identity);
        currentCar.transform.parent = spawnPoint;

        // Set initial rotation
        rotationScrollbar.value = 0f;
        RotateCar();

        // Deactivate all car models
        foreach (GameObject car in carModels)
        {
            car.SetActive(false);
        }

        // Activate the selected car
        carModels[currentIndex].SetActive(true);
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
        currentCar.transform.localRotation = Quaternion.Euler(0f, rotationAmount, 0f);
    }

    void PreviousCar()
    {
        // Update the index to navigate to the previous car
        currentIndex = (currentIndex - 1 + carModels.Length) % carModels.Length;
        Destroy(currentCar);
        currentCar = Instantiate(carModels[currentIndex], spawnPoint.position, Quaternion.identity);
        currentCar.transform.parent = spawnPoint;
        RotateCar();
    }

    void NextCar()
    {
        // Update the index to navigate to the next car
        currentIndex = (currentIndex + 1) % carModels.Length;
        Destroy(currentCar);
        currentCar = Instantiate(carModels[currentIndex], spawnPoint.position, Quaternion.identity);
        currentCar.transform.parent = spawnPoint;
        RotateCar();
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