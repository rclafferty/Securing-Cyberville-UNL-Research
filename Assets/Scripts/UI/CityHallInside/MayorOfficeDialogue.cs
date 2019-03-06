using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Scripts;
using Assets.Scripts.Driving;
using Assets.Scripts.Testing;

public class MayorOfficeDialogue : MonoBehaviour {
    UIBehavior ui;
    ArrayList dialogueDirections;
    Text dialogue;
    [SerializeField]
    string dt;
    [SerializeField]
    int step;
    public bool moveStep;
    Button a1;
    Button a2;
    Text answer1;
    Text answer2;

    GameObject thisObject;

    BinaryTree dialogueTree;
    BinaryTree savepoint;
    
    void Awake()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag(this.tag);
        if (g.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);

        thisObject = this.gameObject;
    }

	// Use this for initialization
	void Start () {
        ui = this.GetComponent<UIBehavior>();
        step = 1;
        moveStep = true;

        SetUpDialogue();
        savepoint = dialogueTree;
        dialogueDirections = new ArrayList();
	}

    void OnLevelWasLoaded()
    {
        if (SceneManager.GetActiveScene().name != "city_hall_inner") return;
        GameObject.Find("Player").GetComponent<PlayerControls>().IsActive(false);
        if (savepoint != dialogueTree)
        {
            Debug.Log("SAVEPOINT != DIALOGUE_TREE");

            dialogueTree = savepoint;
            dialogueDirections.Clear();

            try
            {
                SetUpDialogueGUI();

            }
            catch (UnityEngine.MissingReferenceException mre)
            {
                dialogue = GameObject.Find("Dialogue").GetComponent<Text>();
                answer1 = GameObject.Find("AnswerText1").GetComponent<Text>();
                answer2 = GameObject.Find("AnswerText2").GetComponent<Text>();
                SetUpDialogueGUI();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        dt = dialogue.text;
        
        SetUpDialogueGUI();

        if (SceneManager.GetActiveScene().name == "city_hall_inner")
        {
            //thisObject.SetActive(true);
            this.transform.position = new Vector3(2, 5, -22);
        }
        else
        {
            //thisObject.SetActive(false);
            this.transform.position = new Vector3(0, -20f, 0);
        }
	}
    
    public void Answer1()
    {
        dialogueTree = dialogueTree.left;
        SetUpDialogueGUI();
        moveStep = true;
        dialogueDirections.Add("Left");
        
        if (dialogueTree.step == -2)
        {
            SavepointUpdate();
        }
    }

    public void Answer2()
    {
        dialogueTree = dialogueTree.right;
        SetUpDialogueGUI();
        moveStep = true;
        dialogueDirections.Add("Right");

        if (dialogueTree.step == -2)
        {
            SavepointUpdate();
        }
    }

    void SavepointUpdate()
    {
        for (int i = 0; i < dialogueDirections.Count; i++)
        {
            savepoint.complete = true;
            if ((string)dialogueDirections[i] == "Left")
            {
                savepoint = savepoint.left;
            }
            else
            {
                savepoint = savepoint.right;
            }
        }

        dialogueDirections.Clear();

        if (savepoint != dialogueTree)
            Debug.Log("SAVEPOINT != DIALOGUE_TREE");
    }

    void SetUpDialogue()
    {
        dialogueTree = new BinaryTree();
        dialogueTree.dialogue = "Oh my! Another traveler! I'm too busy for this... Fine, I'll listen. Please be quick about it though.";
        dialogueTree.answer1Text = "I heard you needed help. I'm here to assist in any way I can!";
        dialogueTree.answer2Text = "Nevermind. I have no business here";
        dialogueTree.step = 1;

        //Answer 1 option
        dialogueTree.left = new BinaryTree();
        dialogueTree.right = null;      //Answer 2 is to leave the office
        BinaryTree curr = dialogueTree.left;
        curr.dialogue = "Well, there is one thing... My secretary is out of town today and I'm in need of my medication. I'm far too busy to leave the office."
            + " Could you run to the store to get it for me? The medication is called VirusBeGone.";
        curr.answer1Text = "Of course! I'll head there right now!";
        curr.answer2Text = "No, I'm too busy. Sorry.";
        curr.step = 1;

        //Answer 1 option
        curr.left = new BinaryTree();
        curr.right = null;      //Answer 2 is to leave the office
        curr = curr.left;
        curr.dialogue = "Thank you! Please do hurry back!";
        curr.answer1Text = "See you soon!";
        curr.answer2Text = "";
        curr.step = -2;

        //Answer 1 option
        curr.left = new BinaryTree();
        curr.right = null;      //Answer 2 is to leave the office
        curr = curr.left;
        curr.dialogue = "Have you returned with my medication?";
        curr.answer1Text = "Yes! Here you go.";
        curr.answer2Text = "No, sorry. I'll get right on that.";
        curr.step = 5;

        curr.left = new BinaryTree();
        curr.right = new BinaryTree();
        //Answer 2 option
        BinaryTree curr2 = curr.right;
        curr2.dialogue = "Please do hurry back!";
        curr2.answer1Text = "[Leave]";
        curr2.answer2Text = "";
        curr2.left = curr;
        curr2.right = null;
        curr2.step = 4;
        //Answer 1 option
        curr = curr.left;
        curr.dialogue = "Great! Thank you! Here is your reward!";
        curr.answer1Text = "Thank you very much!";
        curr.answer2Text = "";
        curr.step = -2;

        curr.left = new BinaryTree();
        curr = curr.left;
        curr.dialogue = "Thank you. Please come again for new missions later!";
        curr.answer1Text = "See you then!";
        curr.step = -1;
    }

    void SetUpDialogueGUI()
    {
        dialogue.text = dialogueTree.dialogue;
        answer1.text = dialogueTree.answer1Text;
        answer2.text = dialogueTree.answer2Text;
        step = dialogueTree.step;
        UpdateListener(step);
    }

    public void AssignAnswer1(Button a_1, Text ans1)
    {
        a1 = a_1;
        answer1 = ans1;
        UpdateListener(step);
    }

    public void AssignAnswer2(Button a_2, Text ans2)
    {
        a2 = a_2;
        answer2 = ans2;
        UpdateListener(step);
    }

    public void AssignDialogue(Text d)
    {
        dialogue = d;
    }

    void UpdateListener(int step)
    {
        try
        {
            a1.onClick.RemoveAllListeners();
            a2.onClick.RemoveAllListeners();
        }
        catch (System.NullReferenceException nre)
        {
            Debug.Log("SOMETHING IS NULL");
        }

        switch (step)
        {
            case 1:
                //Debug.Log("CASE 1");
                a1.onClick.AddListener(() => { Answer1(); });
                a2.onClick.AddListener(() => { ui.Load("city_hall_3"); });   //leave
                break;
            case 2:
                //Debug.Log("CASE 2");
                a1.onClick.AddListener(() => { Answer1(); });
                a2.onClick.AddListener(() => { Answer2(); });
                break;
            case 3:
                //Debug.Log("CASE 3");
                a1.onClick.AddListener(() => { Answer1(); });   //reward screen, not answer...
                a2.onClick.AddListener(() => { Answer2(); });
                break;
            case 4:
                //Debug.Log("CASE 4");
                a1.onClick.AddListener(() => { Answer1(); ui.Load("city_hall_3"); });
                a2.onClick.AddListener(() => { Answer2(); });
                break;
            case 5:
                UnityEngine.Events.UnityAction x = () => { CheckItem("VirusBeGone"); };
                a1.onClick.AddListener(x);
                a2.onClick.AddListener(Answer2);
                break;
            case -1:
                //Debug.Log("CASE -1");
                a1.onClick.AddListener(() => { ui.Load("city_hall_3"); });   
                a2.onClick.AddListener(() => { ui.Load("city_hall_3"); });
                break;
            case -2:
                //Debug.Log("CASE -2");
                a1.onClick.AddListener(() => { Answer1(); ui.Load("city_hall_3"); SavepointUpdate(); });
                a2.onClick.AddListener(() => { ui.Load("city_hall_3"); });
                break;
            default:
                Debug.Log("DEFAULT CASE");
                break;
        }
    }

    void CheckItem(string n)
    {
        PlayerControls pc = GameObject.Find("Player").GetComponent<PlayerControls>();
        Backpack b = pc.GetBackpack();
        ArrayList b2 = b.GetBackpack();
        int id = b.LookupItemID(n);
        if (id > 0)
        {
            ((Item)b2[id]).quantity--;
            Answer1();
        }
    }
}
