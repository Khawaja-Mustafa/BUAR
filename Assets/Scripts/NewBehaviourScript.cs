using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public void OpenStudentLoginScene()
    {
        SceneManager.LoadScene("StudentLogin 1");
    }
}
