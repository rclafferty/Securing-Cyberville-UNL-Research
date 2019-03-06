using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour {

    GameObject car;
    //Vector3 car_pos;
    //Quaternion camera_rotation;

    [SerializeField]
    bool driving;
    Rigidbody rb;
    Vector3 stationary;

    void Awake()
    {
        DontDestroyOnLoad(this);
        stationary = new Vector3(0, 5, -26);
    }

	// Use this for initialization
	void Start () {
        driving = false;

        car = GameObject.Find("Player");//.GetComponent<Rigidbody>();
        //Debug.Log("Car's position = " + car.transform.position.ToString());
        rb = car.GetComponent<Rigidbody>();
        Camera.main.farClipPlane = 10000f;
    }

    void OnLevelWasLoaded()
    {
    }

    // Update is called once per frame
    void Update () {
        if (driving)
        {
            //this.transform.position = new Vector3(rb.position.x, rb.position.y, rb.position.z);
            this.transform.localPosition = new Vector3(-55, 90, -130);
        }
        else
        {
            this.transform.position = stationary;

            car.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        if (SceneManager.GetActiveScene().name == "start" || SceneManager.GetActiveScene().name == "home" || SceneManager.GetActiveScene().name == "city_hall_inner")
            driving = false;
        else
            driving = true;

        //Debug.Log("Stationary = " + stationary.ToString());
	}

    public void SetBody(Rigidbody r)
    {
        rb = r;
    }

    public void IsDriving(bool tf)
    {
        if (SceneManager.GetActiveScene().name == "shop")
            Debug.Log("driving set to " + tf);
        driving = tf;
        //rb = r;
        //Camera.main.farClipPlane = 200f;
    }

    public void SetStationary(Vector3 s)
    {
        stationary = s;
        //Debug.Log(stationary.ToString());
        //Camera.main.farClipPlane = 1000f;
    }
}
