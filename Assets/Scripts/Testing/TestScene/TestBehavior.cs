using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Testing;

public class TestBehavior : MonoBehaviour
{
    Car player;
    int spawn;
    Rigidbody r;

    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControls>().GetCarObject();
        player.Active();
        r = player.GetRigidBody();
        spawn = 1;
        Vector3 v = GameObject.Find("SpawnPoint" + spawn).transform.position;
        player.SetPosition(v);
        player.Reset();
        
        Camera.main.GetComponent<CameraMovement>().IsDriving(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
