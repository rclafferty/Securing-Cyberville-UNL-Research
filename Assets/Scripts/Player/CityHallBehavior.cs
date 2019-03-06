using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Scripts.Testing;

public class CityHallBehavior : MonoBehaviour {
    GameObject cityHall;
    Rigidbody r;
    float limit;
    string[] names;
    Text t;

    GameObject ins_text;
    GameObject ins_image;

    Car player;

    //int spawn;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControls>().GetCarObject();
        GameObject.Find("Player").GetComponent<PlayerControls>().IsActive(true);
        r = player.GetRigidBody();
        
        player.SetPosition(GameObject.Find("SpawnPoint1").transform.position);
        player.SetRotation(Vector3.zero);
        player.Reset();
        player.Active();

        Camera.main.GetComponent<CameraMovement>().IsDriving(true);
        
        limit = 300f;
        names = Input.GetJoystickNames();

        ins_image = GameObject.Find("Image");
        ins_text = GameObject.Find("InstructionText");
        ins_image.SetActive(false);
        ins_text.SetActive(false);
	}

    // Update is called once per frame
    void Update () {
		if (Mathf.Abs(r.position.z - this.transform.position.z) < limit
            && Mathf.Abs(r.position.x - this.transform.position.x) < limit)
        {
            //Debug.Log("CLOSE");
            ins_text.SetActive(true);
            ins_image.SetActive(true);
            CityHall();
        }
        else
        {
            ins_text.SetActive(false);
            ins_image.SetActive(false);
        }
    }

    void CityHall()
    {
        t = ins_text.GetComponent<Text>();
        names = Input.GetJoystickNames();
        if (names.Length > 0)
        {
            t.text = "Press Y to enter City Hall";
        }
        else
        {
            t.text = "Press Enter to enter City Hall";
        }

        if (Input.GetButtonDown("Select"))
        {
            SceneManager.LoadScene("city_hall_inner");
        }
    }
}
