using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Testing;

public class Redirect : MonoBehaviour
{
    PlayerControls pc;
    GameObject player;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        Camera.main.GetComponent<CameraMovement>().IsDriving(false);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            pc = player.GetComponent<PlayerControls>();
            Car c = pc.GetCarObject();
            if (c != null)
                SceneManager.LoadScene("start");
                //SceneManager.LoadScene("start_promo");
        }
        catch (System.Exception nre)
        {
            //do nothing
        }
    }
}
