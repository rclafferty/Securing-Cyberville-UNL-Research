using UnityEngine;

using Assets.Scripts.Testing;

public class AIMovement : MonoBehaviour
{
    Car car;
    Rigidbody rb;
    float dead_value;
    float neg_dead_value;

    Vector3 reset_high;
    Vector3 reset_low;

    float accel;

    enum State { forward, backwards, turnleft, turnright };
    State carState;

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }

    void Start()
    {
        car = new Car(this.gameObject, 100f, 500f, 2000f);
        rb = car.GetRigidBody();
        dead_value = 0.1f;
        neg_dead_value = 0 - dead_value;

        reset_low = new Vector3(rb.position.x, rb.position.y, 20f);
        reset_high = new Vector3(rb.position.x, rb.position.y, 4200f);

        carState = State.forward;
    }
    
    void OnLevelWasLoaded()
    {
        //if (car != null)
        //    car.Reset();
    }

    void Update()
    {
        if (rb.position.z >= 4150f || rb.position.z <= 5f)
            Reset();

        if (carState == State.forward)
        {
            car.Accelerate(Vector3.forward /* x1 */);
        }
        else
        {
            car.Decelerate();
        }
    }

    void Reset()
    {
        if (rb.position.z >= 4150f)
            car.SetPosition(reset_low);
        if (rb.position.z <= 5f)
            car.SetPosition(reset_high);

        car.Reset();
    }
}
