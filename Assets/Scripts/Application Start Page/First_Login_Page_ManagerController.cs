using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class First_Login_Page_ManagerController : MonoBehaviour
{
    public void OpenStudentLoginScene()
    {
        SceneManager.LoadScene("StudentLogin 1");
    }
    public void OpenFacultyLoginScene()
    {
        SceneManager.LoadScene("FacultyLogin");
    }
    public void OpenVisitorLoginScene()
    {
        SceneManager.LoadScene("VisitorLogin");
    }
    public void OpenMainScene()
    {
        SceneManager.LoadScene("StartupPage 1");
    }
}
