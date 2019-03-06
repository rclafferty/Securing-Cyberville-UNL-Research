using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuControllerBehavior : MonoBehaviour {

    GameObject resume;
    GameObject quit;
    GameObject save;
    GameObject load;

    string[] names;
    [SerializeField]
    int select;
    float dead;
    float neg_dead;

    float currVal;
    float currValHorizontal;
    float prevVal;
    float prevValHorizontal;

    // Use this for initialization
    void Start () {
        resume = GameObject.Find("ResumeText");
        quit = GameObject.Find("ExitText");
        save = GameObject.Find("SaveText");
        load = GameObject.Find("LoadText");

        names = Input.GetJoystickNames();
        //if (names.Length == 0) this.gameObject.SetActive(false);
        select = 1;
        dead = 0.8f;
        neg_dead = 0 - dead;
    }

    // Update is called once per frame
    void Update()
    {
        currVal = Input.GetAxisRaw("Vertical");
        currValHorizontal = Input.GetAxisRaw("Horizontal");

        if (currVal < neg_dead)
        {
            if (prevVal < neg_dead)
                return;

            SelectDown();
        }
        else if (currVal > dead)
        {
            if (prevVal > dead)
                return;

            SelectUp();
        }
        else if (currValHorizontal > dead) //right
        {
            if (prevValHorizontal > dead)
                return;

            SelectRight();
        }
        else if (currValHorizontal < neg_dead) //left
        {
            if (prevValHorizontal < neg_dead)
                return;

            SelectLeft();
        }

        prevVal = currVal;
    }

    void Select(int i)
    {
        switch(i)
        {
            case 1:
                save.GetComponent<Button>().Select();
                break;
            case 2:
                load.GetComponent<Button>().Select();
                break;
            case 3:
                resume.GetComponent<Button>().Select();
                break;
            case 4:
                quit.GetComponent<Button>().Select();
                break;
        }
    }

    void SelectUp()
    {
        select--;

        if (select < 1)
            select = 1;

        if (select == 1)
        {
            Select(1);
        }
        else
        {
            select = 2;
            Select(2);
        }
    }

    void SelectDown()
    {
        select++;

        if (select > 4)
            select = 4;

        if (select == 2)
        {
            Select(2);
        }
        else
        {
            select = 3;
            Select(3);
        }
    }

    void SelectLeft()
    {
        if (select < 1)
            select = 1;
        else if (select > 4)
            select = 4;

        if (select == 1 || select == 2)
            Select(select);
        else
        {
            select = 3;
            Select(3);
        }
    }

    void SelectRight()
    {
        if (select < 1)
            select = 1;
        else if (select > 4)
            select = 4;

        if (select == 1 || select == 2)
            Select(select);
        else
        {
            select = 4;
            Select(4);
        }
    }
}
