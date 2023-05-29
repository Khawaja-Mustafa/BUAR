using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationFirstPageManager : MonoBehaviour
{
    //public Text usernameText;
    //private LoginManager loginScript;

    //void Start()
    //{
    //    loginScript = FindObjectOfType<LoginManager>();
    //    if (loginScript != null)
    //    {
    //        string username = loginScript.username_1;
    //        usernameText.text = "Welcome, " + username + "!";
    //    }
    //    else
    //    {
    //        Debug.LogError("Login script not found in the scene.");
    //    }
    //}

    public Text usernameText;
    void Start()
    {
        if(LoginManager.username_1 != null)
        {
            usernameText.text = "Welcome, " + LoginManager.username_1 + "!";
        }
        else
        {
            usernameText.text = "Welcome, " + "User" + "!";
        }
        
    }

    public void FacultyInfo()
    {
        SceneManager.LoadScene("CS FACULTY");
    }
    public void GPACalC()
    {
        SceneManager.LoadScene("GPACAL");
    }
    public void Logout()
    {
        SceneManager.LoadScene("StartupPage 1");
    }
}
