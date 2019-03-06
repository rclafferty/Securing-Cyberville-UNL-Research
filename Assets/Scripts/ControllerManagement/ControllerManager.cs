using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerManager : MonoBehaviour {

    string[] names;
    int select;
    float dead;
    float neg_dead;
    GameObject start;
    GameObject quit;

    // Use this for initialization
    void Start() {
        names = Input.GetJoystickNames();
        if (names.Length == 0)
        {
            //this.gameObject.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        select = 1;
        dead = 0.5f;
        neg_dead = 0 - dead;

        start = GameObject.Find("StartText");
        quit = GameObject.Find("QuitText");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Horizontal") > dead)
        {
            SelectRight();
        }
        else if (Input.GetAxis("Horizontal") < neg_dead)
        {
            SelectLeft();
        }
	}

    void SelectLeft()
    {
        select--;
        switch(select)
        {
            case 2:
                quit.GetComponent<Button>().Select();
                break;
            default:
                select = 1;
                start.GetComponent<Button>().Select();
                break;
        }
    }

    void SelectRight()
    {
        select++;
        switch (select)
        {
            case 2:
                start.GetComponent<Button>().Select();
                break;
            default:
                select = 2;
                quit.GetComponent<Button>().Select();
                break;
        }
    }
    
    public void Load(string level)
    {
        SceneManager.LoadScene(level);
    }
    
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
