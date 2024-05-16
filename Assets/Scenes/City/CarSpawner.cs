using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carModels; // Array of car models
    public GameObject[] carPrefabs; // Array of car prefabs with full components
    public Transform spawnPoint; // The spawn point for the car

    void Start()
    {
        // Get the selected car index from PlayerPrefs
        int selectedCarIndex = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        // Instantiate the selected car prefab at the spawn point
        if (selectedCarIndex >= 0 && selectedCarIndex < carModels.Length)
        {
            GameObject selectedCarPrefab = GetCarPrefab(selectedCarIndex);
            if (selectedCarPrefab != null)
            {
                GameObject spawnedCar = Instantiate(selectedCarPrefab, spawnPoint.position, spawnPoint.rotation);
                // Set the tag of the spawned car to "Player"
                spawnedCar.tag = "Player";
            }
            else
            {
                Debug.LogError("Selected car prefab is null!");
            }
        }
        else
        {
            Debug.LogError("Selected car index is out of range!");
        }
    }

    // Determine which prefab to use based on the selected car index
    GameObject GetCarPrefab(int index)
    {
        if (index >= 0 && index < carPrefabs.Length)
        {
            return carPrefabs[index];
        }
        else
        {
            return null; // Index out of range
        }
    }
}
