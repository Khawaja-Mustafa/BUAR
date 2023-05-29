using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FacultyScript_new : MonoBehaviour
{
    public Text headingNameText;
    public Text nameText;
    public Text emailText;
    public Text phoneText;
    public Text researchAreaText;
    public Text publicationsText;
    public Text designationText;
    public Text departmentText;
    public Text universityText;
    public RawImage image;

    public string csvFilePath; // Specify the path to your CSV file

    private void Start()
    {
        LoadDataFromCSV();
    }

    private void LoadDataFromCSV()
    {
        if (!File.Exists(csvFilePath))
        {
            Debug.LogError("CSV file not found at path: " + csvFilePath);
            return;
        }

        try
        {
            using (StreamReader reader = new StreamReader(csvFilePath))
            {
                // Read and ignore the header line
                reader.ReadLine();

                // Read the remaining lines and process the data
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    // Assign the data to the UI elements
                    string[] data = line.Split(',');
                    if (data.Length >= 10)
                    {
                        headingNameText.text = data[0];
                        nameText.text = data[1];
                        emailText.text = data[2];
                        phoneText.text = data[3];
                        researchAreaText.text = data[4];
                        publicationsText.text = data[5];
                        designationText.text = data[6];
                        departmentText.text = data[7];
                        universityText.text = data[8];

                        // Load and display the image
                        StartCoroutine(LoadImageFromURL(data[9]));
                    }
                    else
                    {
                        Debug.LogWarning("Invalid CSV line: " + line);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Debug.Log("Error reading CSV file: " + e.Message);
        }
    }

    private IEnumerator LoadImageFromURL(string url)
    {
        using (WWW www = new WWW(url))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                // Get the downloaded texture
                Texture2D texture = www.texture;

                // Assign the texture to the RawImage component
                image.texture = texture;
            }
            else
            {
                Debug.Log("Image download failed: " + www.error);
            }
        }
    }
}
