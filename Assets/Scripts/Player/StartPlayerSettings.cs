using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.UserSessionLogger;

public class StartPlayerSettings : MonoBehaviour {

    GameObject user;
    GameObject u2;
    GameObject feedback;
    GameObject submit;

    void Start () {
        //GameObject.Find("Player").GetComponent<PlayerControls>().IsActive(false);
        u2 = GameObject.Find("UsernameInputField");
        user = GameObject.Find("Input");
        u2.SetActive(false);
        feedback = GameObject.Find("FeedbackText");
        feedback.SetActive(false);
        submit = GameObject.Find("SubmitButton");
        submit.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Username()
    {
        GameObject.Find("StartText").SetActive(false);
        GameObject.Find("QuitText").SetActive(false);
        u2.SetActive(true);
        //feedback.SetActive(true);
        submit.SetActive(true);
    }

    public void SetUsername()
    {
        string username = user.GetComponent<Text>().text;
        GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>().LogToDatabase("Player " + username + " set username", UserSessionLoggerBehavior.UserAction.Setup);

        if (!string.IsNullOrEmpty(username))
        {
            GameObject.Find("Player").GetComponent<PlayerControls>().SetUsername(username);
            GameObject.Find("Canvas").GetComponent<UIBehavior>().Load("home");
            Valid(username);
        }
        else
        {
            feedback.SetActive(true);
            feedback.GetComponent<Text>().text = "Invalid Username";
            GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>().LogToDatabase("Player entered invalid username", UserSessionLoggerBehavior.UserAction.Setup);
        }
    }

    void Valid(string u)
    {
        //System.Threading.Thread.Sleep(2000);
    }
}
