using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableController : MonoBehaviour
{
    public GameObject rowPrefab; // Prefab for a row in the table
    public Transform tableParent; // Parent object for the table rows

    // Example data
    private string[,] data = {
        {"1", "John Doe", "Manager"},
        {"2", "Jane Smith", "Developer"},
        {"3", "Bob Johnson", "Designer"}
    };

    private void Start()
    {
        // Create the header row
        GameObject headerRow = Instantiate(rowPrefab, tableParent);

        Text headerText = headerRow.GetComponentInChildren<Text>();
        if (headerText != null)
        {
            headerText.text = "Serial Number\tName\tDesignation";
        }
        else
        {
            Debug.LogError("No Text component found on header row!");
        }

        // Create a row for each data item
        for (int i = 0; i < data.GetLength(0); i++)
        {
            // Create the row object
            GameObject row = Instantiate(rowPrefab, tableParent);

            // Set the text of each column in the row
            row.transform.GetChild(0).GetComponent<Text>().text = data[i, 0]; // Serial Number
            row.transform.GetChild(1).GetComponent<Text>().text = data[i, 1]; // Name
            row.transform.GetChild(2).GetComponent<Text>().text = data[i, 2]; // Designation
        }
    }
}
