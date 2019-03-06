using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

using Campaign;

using Assets.Scripts.UserSessionLogger;

public class MayorDialogue : MonoBehaviour {

    ConversationTree ct;

    Button a1;
    Button a2;
    Button a3;
    Text answer1;
    Text answer2;
    Text answer3;
    Text dialogue;

    // Use this for initialization
    void Start () {
        GameObject g = GameObject.Find("CampaignManager");
        CampaignManager cm = g.GetComponent<CampaignManager>();
        ct = cm.GetConversation(SpeakerType.Mayor, 1);
        //ct = GameObject.Find("CampaignManager").GetComponent<CampaignManager>().GetConversation(SpeakerType.Mayor, 1);
        dialogue = GameObject.Find("Dialogue").GetComponent<Text>();
        answer1 = GameObject.Find("AnswerText1").GetComponent<Text>();
        answer2 = GameObject.Find("AnswerText2").GetComponent<Text>();
        answer3 = GameObject.Find("AnswerText3").GetComponent<Text>();
        a1 = GameObject.Find("AnswerText1").GetComponent<Button>();
        a2 = GameObject.Find("AnswerText2").GetComponent<Button>();
        a3 = GameObject.Find("AnswerText3").GetComponent<Button>();

        UpdateDialogue();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void UpdateDialogue()
    {
        dialogue.text = ct.SpeakerText;
        answer1.text = ct.Responses[0];
        try
        {
            answer2.text = ct.Responses[1];
        }
        catch (System.Exception nre)
        {
            answer2.text = "";
        }
        try
        {
            answer3.text = ct.Responses[2];
        }
        catch (System.Exception nre)
        {
            answer3.text = "";
        }
    }

    public void AssignAnswer1(Button a_1, Text ans1)
    {
        a1 = a_1;
        answer1 = ans1;
    }

    public void AssignAnswer2(Button a_2, Text ans2)
    {
        a2 = a_2;
        answer2 = ans2;
    }

    public void AssignAnswer3(Button a_3, Text ans3)
    {
        a3 = a_3;
        answer3 = ans3;
    }

    public void AnswerClick(int i) // 1, 2, or 3
    {
        UserSessionLoggerBehavior uslb = GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>();
        uslb.LogToDatabase("Player selects mayor option --> " + GameObject.Find("AnswerText" + i).GetComponent<Text>().text, UserSessionLoggerBehavior.UserAction.LeftClickButton);

        ct = ct.NextTrees[i-1];
        if (ct == null || ct.SpeakerText == null)
        {
            SceneManager.LoadScene("city_hall_3");
            return;
        }

        Debug.Log("SpeakerText = " + ct.SpeakerText);
        while (ct.SpeakerText[0] == '/')
        {
            Debug.Log("SpeakerText = " + ct.SpeakerText);
            //SE -- SEND EVENT
            string s = ct.SpeakerText.Substring(4);
            GameObject.Find("CampaignManager").GetComponent<CampaignManager>().CheckEvent(new SpeakEvent(SpeakerType.Mayor, 1, s));
            ct = ct.NextTrees[0];

            if (ct == null || ct.SpeakerText == null)
            {
                SceneManager.LoadScene("city_hall_3");
                return;
            }
        }
        
        UpdateDialogue();
    }
}
