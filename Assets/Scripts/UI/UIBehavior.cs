using UnityEngine;
using UnityEngine.SceneManagement;

using Assets.Scripts.UserSessionLogger;
using Assets.Scripts.Testing;

public class UIBehavior : MonoBehaviour {

    //LevelManagerBehavior lmb;
    GameObject player;

	// Use this for initialization
	void Start () {
        try
        {
            Camera.main.GetComponent<CameraMovement>().IsDriving(false);
        }
        catch (System.NullReferenceException nre)
        {
            Debug.Log("NRE -- Is this debug mode?");
        }
    }
	
	// Update is called once per frame
	void Update () {

    }

    private string ToTitleCase(string message)
    {
        return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(message.ToLower());
    }

    public void Load(string level)
    {
        string name = SceneManager.GetActiveScene().name;
        UserSessionLoggerBehavior uslb = GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>();
        string newLevel = level;
        if (level == "city_hall_3")
        {
            newLevel = "City Hall";
        }
        uslb.LogToDatabase("Player enters " + ToTitleCase(newLevel), UserSessionLoggerBehavior.UserAction.SceneChange);
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        UserSessionLoggerBehavior uslb = GameObject.Find("UserSessionLogger").GetComponent<UserSessionLoggerBehavior>();
        uslb.LogToDatabase("Player quit game", UserSessionLoggerBehavior.UserAction.SceneChange);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
