using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class LoginManager : MonoBehaviour
{
    private InputField usernameInputField;
    private InputField passwordInputField;
    private InputField roleInputField;
    private InputField instituteInputField;
    private string cookieString;
    //Application MainMenu Username
    public static string username_1 = "Default";



    private void Awake()
    {
        // Manually reference the input fields
        usernameInputField = GameObject.Find("Username").GetComponent<InputField>();
        passwordInputField = GameObject.Find("Password").GetComponent<InputField>();
        roleInputField = GameObject.Find("Role").GetComponent<InputField>();
        instituteInputField = GameObject.Find("Institute").GetComponent<InputField>();
    }

    public void LoginButtonClicked()
    {
        //string username = usernameInputField.text;
        //string password = passwordInputField.text;
        //string role = roleInputField.text;
        //string institute = instituteInputField.text;

        string username = "03-134201-034";
        string password = "khawaja@123";
        string role = "Student";
        string institute = "Lahore Campus";
        Debug.Log(username);

        //Application MainMenu Username
        username_1 = username;


        StartCoroutine(LoginToUniversityCMS(username, password, role, institute));
    }

    private IEnumerator LoginToUniversityCMS(string username, string password, string role, string institute)
    {
        // Set up the URL for the University CMS login page
        string url = "https://cms.bahria.edu.pk/Logins/Student/Login.aspx";

        // Set up the form data for the POST request
        WWWForm form = new WWWForm();
        form.AddField("ctl00$BodyPH$tbEnrollment", username);
        form.AddField("ctl00$BodyPH$tbPassword", password);
        form.AddField("ctl00$BodyPH$ddlSubUserType", role);
        form.AddField("ctl00$BodyPH$ddlInstituteID", institute);

        // Set up the UnityWebRequest
        UnityWebRequest request = UnityWebRequest.Post(url, form);

        // Send the POST request
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Check if the login was successful
            if (request.downloadHandler.text.Contains("Login Failed"))
            {
                Debug.LogError("Login failed. Please check your username and password.");
            }
            else
            {
                Debug.Log("Login successful.");

                // Get the session cookie from the response headers
                cookieString = request.GetResponseHeader("Set-Cookie");

                
                // Call the GetDataFromWebsite() function to fetch data after successful login
                yield return StartCoroutine(GetDataFromWebsite());

                SceneManager.LoadScene("ApplicationFirstPage");
            }
        }
        else
        {
            Debug.LogError("Error calling University CMS API: " + request.error);
        }
    }

    private IEnumerator GetDataFromWebsite()
    {
        // Set up the URL for the student profile page
        string url1 = "https://cms.bahria.edu.pk/Sys/Student/Profile.aspx";

        // Set up the UnityWebRequest
        UnityWebRequest request = UnityWebRequest.Get(url1);

        if (!string.IsNullOrEmpty(cookieString))
        {
            // Set the session cookie in the request headers
            request.SetRequestHeader("Cookie", cookieString);
        }

        // Send the GET request
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data successfully fetched from website.");
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error fetching data from website: " + request.error);
        }
    }

    //private IEnumerator GetDataFromWebsite()
    //{
    //    // Set up the URL for the student profile page
    //    string url = "https://cms.bahria.edu.pk/Sys/Student/Dashboard.aspx";

    //    // Set up the UnityWebRequest
    //    UnityWebRequest request = UnityWebRequest.Get(url);

    //    request.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

    //    // Check if the cookieString variable is null before setting the request headers
    //    if (!string.IsNullOrEmpty(cookieString))
    //    {
    //        request.SetRequestHeader("Cookie", cookieString);
    //    }

    //    // Send the GET request
    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        // Check if the user has been redirected to the login page
    //        if (request.url.Contains("Login.aspx"))
    //        {
    //            Debug.LogError("Session expired. Please log in again.");
    //        }
    //        else
    //        {
    //            // Navigate to the student profile page
    //            url = "https://cms.bahria.edu.pk/Sys/Student/Profile.aspx";
    //            request = UnityWebRequest.Get(url);
    //            request.SetRequestHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

    //            if (!string.IsNullOrEmpty(cookieString))
    //            {
    //                request.SetRequestHeader("Cookie", cookieString);
    //            }

    //            yield return request.SendWebRequest();

    //            if (request.result == UnityWebRequest.Result.Success)
    //            {
    //                Debug.Log("Successfully navigated to student profile page.");
    //                Debug.Log(request.downloadHandler.text);

    //                // Retrieve data from the student profile page
    //                // TODO: Implement data extraction logic
    //            }
    //            else
    //            {
    //                Debug.LogError("Error navigating to student profile page: " + request.error);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Error fetching data from website: " + request.error);
    //    }
    //}



}
