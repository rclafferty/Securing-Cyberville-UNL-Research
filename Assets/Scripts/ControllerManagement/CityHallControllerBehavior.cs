using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CityHallControllerBehavior : MonoBehaviour {
    GameObject answer1;
    GameObject answer2;
    GameObject leave;

    string[] names;
    [SerializeField]
    int select;
    float dead;
    float neg_dead;

    float prevVal;
    float currVal;

    // Use this for initialization
    void Start()
    {
        answer1 = GameObject.Find("AnswerText1");
        answer2 = GameObject.Find("AnswerText2");
        leave = GameObject.Find("ExitText");

        names = Input.GetJoystickNames();
        if (names.Length == 0) this.gameObject.SetActive(false);
        select = 1;
        dead = 0.8f;
        neg_dead = 0 - dead;
    }

    // Update is called once per frame
    void Update()
    {
        currVal = Input.GetAxisRaw("Vertical");
        currVal = Mathf.Round(currVal * 100f) / 100f;
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

        prevVal = currVal;
    }

    void SelectUp()
    {
        select--;
     
        if (select == 2) Debug.Log("2");

        if (select == 0 || select == 1)
        {
            select = 1;
            answer1.GetComponent<Button>().Select();
        }
        else if (select == 2)
        {
            answer2.GetComponent<Button>().Select();
        }
        else
        {
            select = 3;
            leave.GetComponent<Button>().Select();
        }
    }

    void SelectDown()
    {
        select++;
        switch (select)
        {
            case 2:
                select = 2;
                answer2.GetComponent<Button>().Select();
                break;
            case 0:
            case 1:
                select = 1;
                answer1.GetComponent<Button>().Select();
                break;
            case 3:
            case 4:
                select = 3;
                leave.GetComponent<Button>().Select();
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
