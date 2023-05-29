using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using HtmlAgilityPack;
using System.Net;
using UnityEngine.UI;

public class Scrapper : MonoBehaviour
{
    public Text outputText;
    //public Text OutSerialnum;
    //public Text OutName;
    //public Text OutDesignation;

    private void Start()
    {
        try
        {
            // Create a new HtmlWeb object to load the website
            HtmlWeb web = new HtmlWeb();

            // Load the website and get its HTML document
            HtmlDocument doc = web.Load("https://bahria.edu.pk/bulc/cs/cs-faculty/");

            // Get the list of all the faculty members
            HtmlNodeCollection facultyMembers = doc.DocumentNode.SelectNodes("//div[@class='box details']/div[@class='description']/table/tbody/tr");

            // Loop through each faculty member and extract the required data
            if (facultyMembers != null)
            {
                bool isFirstIteration = true;

                foreach (HtmlNode facultyMember in facultyMembers)
                {
                    // Extract the name of the faculty member
                    string serialnum = facultyMember.SelectSingleNode(".//td[1]").InnerText.Trim();

                    // Extract the designation of the faculty member
                    string name = facultyMember.SelectSingleNode(".//td[2]").InnerText.Trim();

                    HtmlNode linkNode = facultyMember.SelectSingleNode(".//td[2]//a");
                    string link = linkNode != null ? linkNode.Attributes["href"].Value : "";
                    //string link = facultyMember.SelectSingleNode(".//td[2]//p//span//a").Attributes["href"].Value;

                    // Extract the link to the full information of the faculty member
                    string designation = facultyMember.SelectSingleNode(".//td[3]").InnerText.Trim();

                    // Update the Text UI element with the scraped data, skipping the first iteration
                    if (isFirstIteration)
                    {
                        isFirstIteration = false;
                    }
                    else
                    {
                        outputText.text += serialnum + "    " + name + " (" + designation + ")\n\n";
                        Debug.Log(link);
                        //OutSerialnum.text += serialnum + "\n\n";
                        //OutName.text += name + "\n\n";
                        //OutDesignation.text += designation + "\n\n";

                    }
                }

            }
        }
        catch (WebException e)
        {
            Debug.Log("Network error occurred: " + e.Message);
        }
    }
}