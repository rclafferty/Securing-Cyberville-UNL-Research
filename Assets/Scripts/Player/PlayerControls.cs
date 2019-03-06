using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using Assets.Scripts;
using Assets.Scripts.Driving;
using Assets.Scripts.Testing;
using Campaign;


public class PlayerControls : MonoBehaviour
{
    string username;
    Car player;
    Rigidbody body;
    float dead_value;
    float neg_dead_value;

    [SerializeField]
    float accel;
    //[SerializeField]
    //float mag_z;
    //[SerializeField]
    //float power;
    //[SerializeField]
    //float power_cap;
    [SerializeField]
    float turn;
    //[SerializeField]
    //float turn2;
    //[SerializeField]
    //bool isTurn2GreaterThanDeadValue;

    //[SerializeField]
    //Vector3 ang_vel;

    Vector3 gravity;

    Backpack backpack;
    bool backpackShow;

    GameObject pauseMenu;
    bool isActive;

    void Awake()
    {
        //GameObject[] go = GameObject.FindGameObjectsWithTag("Player");
        /*if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            Destroy(this);
        else
            DontDestroyOnLoad(this);*/

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        username = "Player";
        player = new Car(this.gameObject, 10000f, 20000f, 2000f);
        IsActive(false);
        if (player == null) Debug.Log("WHY IS THIS NULL?!??!?!??!");
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);

        backpackShow = false;
        body = player.GetRigidBody();

        dead_value = 0.1f;
        neg_dead_value = 0 - dead_value;

        backpack = new Backpack();
        backpack.HideBag();

#if UNITY_EDITOR
        ArrayList cheat = new ArrayList();
        cheat.Add(new Item(0, 1));
        cheat.Add(new Item(1, 2));
        cheat.Add(new Item(2, 3));
        cheat.Add(new Item(3, 4));
        cheat.Add(new Item(4, 500));
        backpack.CheatSetBackpack(cheat);
#endif

    }
    
    /*void OnLevelWasLoaded()
    {
        if (player != null)
            player.Reset();
    }*/

    public string Username
    {
        get
        {
            return username;
        }
    }

    public void SetUsername(string u)
    {
        username = u;
    }

    void Update()
    {
        if (isActive)
        {
            accel = Input.GetAxisRaw("Vertical") * 1.2f;
            turn = Input.GetAxisRaw("Horizontal") * 2f;

            if (Mathf.Abs(accel) > dead_value)
            {
                player.Accelerate(Vector3.forward * accel);

                if (Mathf.Abs(turn) > dead_value)
                {
                    if (accel > 0)
                        player.Turn(Vector3.up * turn);
                    else
                        player.Turn(Vector3.up * -1 * turn);
                }
            }
            else
            {
                player.Decelerate();
            }

            SetGravity(new Vector3(0, -1000f/* / body.position.y*/, 0));
            player.ApplyAbsForce(gravity);
        }

            if (Input.GetButtonDown("Backpack"))// || Input.GetButtonDown("Pause"))
            {
                backpackShow = !backpackShow;
                string n = SceneManager.GetActiveScene().name;
                if (n != "home" && n != "start" && n != "city_hall_inner")
                {
                    if (backpackShow)
                    {
                        backpack.ShowBag();
                        IsActive(false);
                    }
                    else
                    {
                        backpack.HideBag();
                        IsActive(true);
                    }
                }
            }

            if (Input.GetButtonDown("Pause"))
            {
                string n = SceneManager.GetActiveScene().name;
                if (n != "home" && n != "start" && n != "city_hall_inner")
                    ShowPauseMenu();
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Backslash) || Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                player.Reset();
            }
            try
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Joystick1Button4))
                {
                    player.Reset();
                    player.SetPosition(GameObject.Find("SpawnPoint1").transform.position);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    player.Reset();
                    player.SetPosition(GameObject.Find("SpawnPoint2").transform.position);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    player.Reset();
                    player.SetPosition(GameObject.Find("SpawnPoint3").transform.position);
                }
                if (Input.GetKeyDown(KeyCode.Period))
                {
                    SceneManager.LoadScene("shop");
                }
            }
            catch (System.NullReferenceException nre)
            {
                Debug.Log("SPAWNPOINT DOES NOT EXIST");
            }
#endif
        
    }

    public void IsActive(bool tf)
    {
        isActive = tf;
        if (tf)
            player.Active();
        else
            player.Inactive();
    }

    public Car GetCarObject()
    {
        while(player == null)
        {
            //busy wait
            Debug.Log("PLAYER IS NULL");
        }

        return player;
    }

    void SetGravity(Vector3 v)
    {
        gravity = v;
    }

    public void PickupItem(string item, int num)
    {
        backpack.Pickup(backpack.LookupItemID(item), num);
        if (item == "Money(Clone)" || item == "m")
            item = "Money";
        GameObject.Find("CampaignManager").GetComponent<CampaignManager>().CheckEvent(new PickupEvent(item, "Player"));
    }

    public void ShowPauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            player.Active();
            
            if (Input.GetJoystickNames().Length != 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            pauseMenu.SetActive(true);
            player.Pause(true);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadStart()
    {
        SceneManager.LoadScene("start");
    }

    public Backpack GetBackpack()
    {
        return backpack;
    }
}
