using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDuckScript : MonoBehaviour
{
    public List<GameObject> duckModels = new List<GameObject>(); // List of all available ducks
    public List<GameObject> selectedDucks = new List<GameObject>(); // List of selected ducks

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
        Debug.Log("Game started with new ducks!");
    }
    private void Update()
    {
        if (currentPersons.Count < initialSelectionCount && duckModels.Count >= 1)
        {
            AddNewDuckToKeepUpdated();
        }
    }
    public void AddNewDuckToKeepUpdated()
    {
        GameObject selectedDuck = duckModels[0];
        currentPersons.Add(selectedDuck);
        selectedDuck.SetActive(true);
        ShuffleArray(duckModels);
    }

    public void RemoveDuck(GameObject duck)
    {
        /*
        selectedDucks.Remove(duck);
        duckModels.Remove(duck);
        currentPersons.Remove(duck);
        */
        
        // Remove the duck from duckModels
        if (duckModels.Contains(duck))
        {
            duckModels.Remove(duck);
            Debug.Log($"{duck.name} removed from duckModels.");
        }
        // Remove the duck from selectedDucks
        if (selectedDucks.Contains(duck))
        {
            selectedDucks.Remove(duck);
            Debug.Log($"{duck.name} removed from selectedDucks.");
        }
        // Remove the duck from currentPerson
        if (currentPersons.Contains(duck))
        {
            currentPersons.Remove(duck);
            Debug.Log($"{duck.name} removed from currentDucks.");
        }
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
            duckModels.Remove(duck);
        }

        if (currentPersons.Count == 0)
        {
            Debug.LogWarning("No ducks selected at the start.");
        }
    }
}
