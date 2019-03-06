using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Scripts.Testing;

public class ShopBehavior : MonoBehaviour {
    GameObject cityHall;
    Rigidbody r;
    float limit;
    string[] names;
    Text t;
    Image i;

    GameObject ins_text;
    GameObject ins_image;

    Car player;

    int spawn;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControls>().GetCarObject();
        player.Active();
        r = player.GetRigidBody();
        /*spawn = 1;
        Vector3 v = GameObject.Find("SpawnPoint" + spawn).transform.position;
        player.SetPosition(v);
        player.SetRotation(Vector3.zero);
        player.Reset();*/

        //Camera.main.GetComponent<CameraMovement>().IsDriving(true);

        limit = 300f;
        //names = Input.GetJoystickNames();

        ins_image = GameObject.Find("Image_shop");
        ins_text = GameObject.Find("InstructionText_shop");
        //ins_image.SetActive(false);
        //ins_text.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(r.position.z - this.transform.position.z) < limit
            && Mathf.Abs(r.position.x - this.transform.position.x) < limit)
        {
            //Debug.Log("CLOSE");
            ins_text.SetActive(true);
            ins_image.SetActive(true);
            EnterShop();
        }
        else
        {
            ins_text.SetActive(false);
            ins_image.SetActive(false);
        }
    }

    void EnterShop()
    {
        t = ins_text.GetComponent<Text>();
        names = Input.GetJoystickNames();
        if (names.Length > 0)
        {
            t.text = "Press Y to enter shop";
        }
        else
        {
            t.text = "Press Enter to enter shop";
        }

        if (Input.GetButtonDown("Select"))
        {
            SceneManager.LoadScene("shop");
            //player.IsActive(false);
            GameObject.Find("Player").GetComponent<PlayerControls>().IsActive(false);
        }
    }
}
