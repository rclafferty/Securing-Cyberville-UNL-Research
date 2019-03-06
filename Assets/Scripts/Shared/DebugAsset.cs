using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugAsset : MonoBehaviour
{

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F12))
        {
            SceneManager.LoadScene("test");
        }
        else if (Input.GetKeyDown(KeyCode.F11))
        {
            SceneManager.LoadScene("home");
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            SceneManager.LoadScene("city_hall_3");
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            SceneManager.LoadScene("shop");
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            Campaign.WinCondition w = new Campaign.WinCondition();
            w.InitializeMission();
            //SceneManager.LoadScene("end");
        }
#endif
    }
}
