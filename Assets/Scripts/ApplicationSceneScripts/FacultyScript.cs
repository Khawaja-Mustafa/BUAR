using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using HtmlAgilityPack;
using System.Net;
using UnityEngine.UI;
using UnityEngine.Networking;

public class FacultyScript : MonoBehaviour
{
    public Text outputText;
    public RawImage image;

    private void Start()
    {
        try
        {
            // Create a new HtmlWeb object to load the website
            HtmlWeb web = new HtmlWeb();

            // Load the website and get its HTML document
            HtmlDocument doc = web.Load("https://bahria.edu.pk/bulc/cs/dr-iram-noreen/");

            // Find the second img tag on the page
            HtmlNodeCollection imageTags = doc.DocumentNode.SelectNodes("//img");

            if (imageTags != null && imageTags.Count >= 2)
            {
                HtmlNode imageTag = imageTags[1]; // Index 1 for the second img tag

                // Extract the image URL
                string imageUrl = imageTag.Attributes["src"].Value;

                // Update the Text UI element with the scraped data
                outputText.text = "Image URL: " + imageUrl + "\n\n";

                // Start the coroutine to load and display the image
                StartCoroutine(LoadImageFromURL(imageUrl));
            }
            else
            {
                outputText.text = "Image not found.";
            }
        }
        catch (WebException e)
        {
            Debug.Log("Network error occurred: " + e.Message);
        }
    }

    private IEnumerator LoadImageFromURL(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Get the downloaded texture
                Texture2D texture = DownloadHandlerTexture.GetContent(www);

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
