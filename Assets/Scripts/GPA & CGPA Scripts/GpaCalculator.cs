using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GpaCalculator : MonoBehaviour
{
    public InputField gpaField;
    public RowData[] rowsData;

    private void Start()
    {
        // Bind GPA field to calculation function
        //gpaField = GameObject.FindWithTag("GPAField").GetComponent<InputField>();
        gpaField.onValueChanged.AddListener(delegate { CalculateGpa(); });
        //gpaField = GameObject.FindWithTag("GPAField").GetComponent<InputField>();
        //if (gpaField != null)
        //{
        //    gpaField.onValueChanged.AddListener(delegate { CalculateGpa(); });
        //}
        //else
        //{
        //    Debug.LogError("Could not find GPAField with tag 'GPAField'.");
        //}

        // Find all the RowData objects in the scene
        rowsData = FindObjectsOfType<RowData>();
        foreach (var rowData in rowsData)
        {
            // Set input field callbacks
            rowData.creditHoursField.onValueChanged.AddListener(delegate { UpdateGradePoints(); });
            rowData.subjectField.onValueChanged.AddListener(delegate { UpdateGradePoints(); });
            rowData.marksField.onValueChanged.AddListener(delegate { UpdateGradePoints(); });

            // Set default values
            rowData.creditHoursField.text = "0";
            rowData.subjectField.text = "";
            rowData.marksField.text = "0";
            rowData.gradePointsText.text = "0";
        }
    }

    private void CalculateGpa()
    {
        float totalGradePoints = 0;
        float totalCreditHours = 0;

        // Calculate total grade points and credit hours
        foreach (var rowData in rowsData)
        {
            totalGradePoints += rowData.gradePoints;
            totalCreditHours += rowData.creditHours;
        }

        // Calculate GPA
        float gpa = totalGradePoints / totalCreditHours;

        // Update GPA field
        gpaField.text = gpa.ToString("F2");
    }

    private void UpdateGradePoints()
    {
        float totalGradePoints = 0;
        float totalCreditHours = 0;

        // Calculate grade points and credit hours for each row
        foreach (var rowData in rowsData)
        {
            float.TryParse(rowData.creditHoursField.text, out rowData.creditHours);
            float.TryParse(rowData.marksField.text, out rowData.marks);

            // Calculate grade points based on marks
            if (rowData.marks >= 90) rowData.gradePoints = 4;
            else if (rowData.marks >= 80) rowData.gradePoints = 3;
            else if (rowData.marks >= 70) rowData.gradePoints = 2;
            else if (rowData.marks >= 60) rowData.gradePoints = 1;
            else rowData.gradePoints = 0;

            // Update grade points text
            rowData.gradePointsText.text = rowData.gradePoints.ToString("F2");

            // Update total grade points and credit hours
            totalGradePoints += rowData.gradePoints;
            totalCreditHours += rowData.creditHours;
        }

        // Calculate GPA
        float gpa = totalGradePoints / totalCreditHours;

        // Update GPA field
        gpaField.text = gpa.ToString("F2");
    }

    [System.Serializable]
    public class RowData : MonoBehaviour
    {
        public float creditHours = 0;
        public string subject = "";
        public float marks = 0;
        public float gradePoints = 0;

        [HideInInspector]
        public InputField creditHoursField;
        [HideInInspector]
        public InputField subjectField;
        [HideInInspector]
        public InputField marksField;
        [HideInInspector]
        public Text gradePointsText;
    }
}
