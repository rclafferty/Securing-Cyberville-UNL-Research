using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeControllerBehavior : MonoBehaviour {

    GameObject map;
    GameObject helpers;
    GameObject leave;

    string[] names;
    [SerializeField]
    int select;
    float dead;
    float neg_dead;

    // Use this for initialization
    void Start () {
        map = GameObject.Find("Map");
        helpers = GameObject.Find("Helper");
        leave = GameObject.Find("ExitText");

        names = Input.GetJoystickNames();
        //if (names.Length == 0) this.gameObject.SetActive(false);
        select = 1;
        dead = 0.5f;
        neg_dead = 0 - dead;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > dead)
        {
            SelectRight();
        }
        else if (Input.GetAxis("Horizontal") < neg_dead)
        {
            SelectLeft();
        }
        else if (Input.GetAxis("Vertical") < neg_dead)
        {
            SelectDown();
        }
        else if (Input.GetAxis("Vertical") > dead)
        {
            SelectUp();
        }
    }

    void SelectLeft()
    {
        if (select == 3) return; // Leave
        else
        {
            select = 1;
            map.GetComponent<Button>().Select();
        }
    }

    void SelectRight()
    {
        if (select == 3) return; // Leave
        else
        {
            select = 2;
            helpers.GetComponent<Button>().Select();
        }
    }

    void SelectUp()
    {
        if (select == 2) return; // Helpers
        else
        {
            select = 1;
            map.GetComponent<Button>().Select();
        }
    }

    void SelectDown()
    {
        select = 3;
        leave.GetComponent<Button>().Select();
    }
}
