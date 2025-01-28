using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDuckScript : MonoBehaviour
{
    public List<GameObject> duckModels = new List<GameObject>(); // List of all available ducks
    private List<GameObject> selectedDucks = new List<GameObject>(); // List of selected ducks

    public int initialSelectionCount = 3; // Number of ducks to select at the beginning
    public List<GameObject> currentPersons = new List<GameObject>(); // The currently active ducks

    private List<GameObject> SelectInitialDucks(int count)
    {
        // Shuffle the ducks to randomise the selection
        ShuffleArray(duckModels);

        List<GameObject> initiallySelected = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            if (duckModels.Count == 0)
            {
                Debug.LogWarning("Not enough ducks to select.");
                break;
            }

            // Select the first duck from the shuffled list
            GameObject selectedDuck = duckModels[0];
            initiallySelected.Add(selectedDuck);

            // Remove the selected duck from the available list
            duckModels.RemoveAt(0);
        }

        return initiallySelected;
    }
    private void Update()
    {
        if (currentPersons.Count < initialSelectionCount)
        {
            GameObject selectedDuck = duckModels[0];
            currentPersons.Add(selectedDuck);
            selectedDuck.SetActive(true);
            ShuffleArray(duckModels);
        }
    }

    private GameObject SelectRandomPerson()
    {
        if (duckModels.Count == 0)
        {
            // Handle the case where no ducks are left to select
            AllDucksSelected();
            return null;
        }

        // Select a random duck
        int randomIndex = Random.Range(0, duckModels.Count);
        GameObject selectedDuck = duckModels[randomIndex];

        // Remove the selected duck from the available list
        duckModels.RemoveAt(randomIndex);

        // Add it to the list of selected ducks
        selectedDucks.Add(selectedDuck);

        Beginning(); // Start game logic
        return selectedDuck;
    }

    private void ShuffleArray(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void Beginning()
    {
        // Placeholder for game start logic
        Debug.Log("Game started with new ducks!");
    }

    private void AllDucksSelected()
    {
        // Placeholder for handling all ducks selected
        Debug.Log("All ducks have been selected!");
    }

    private void Start()
    {
        // Select the initial number of ducks
        selectedDucks = SelectInitialDucks(initialSelectionCount);

        // Log the initially selected ducks
        Debug.Log("Initially selected ducks:");
        foreach (GameObject duck in selectedDucks)
        {
            Debug.Log(duck.name);
        }

        // Set all selected ducks as active
        foreach (GameObject duck in selectedDucks)
        {
            duck.SetActive(true);
            currentPersons.Add(duck); // Add to the list of currently active ducks
        }

        if (currentPersons.Count == 0)
        {
            Debug.LogWarning("No ducks selected at the start.");
        }
    }
}
