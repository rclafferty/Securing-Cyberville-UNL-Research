using UnityEngine;

public class AIMovement_Tunnel : MonoBehaviour {

    //float dead_value;
    //float neg_dead_value;
    Rigidbody rb;

    [SerializeField]
    float power;
    [SerializeField]
    float power_cap;
    [SerializeField]
    float turnPower;
    [SerializeField]
    float carModifier;

    [SerializeField]
    float vertical;
    [SerializeField]
    float viewX;
    float viewY;
    float horizontal;

    [SerializeField]
    float mag;

    enum State {forward, backwards, turnleft, turnright };
    State carState = State.forward;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();

        carModifier = rb.transform.localScale.z * 1000;
        power = carModifier;// * 200f;
        power_cap = power * 5;
        turnPower = power * 0.33f;
    }
	
	// Update is called once per frame
	void Update () {
        mag = rb.velocity.z;
        if (rb.position.z >= 4300f) rb.position = new Vector3(rb.position.x, rb.position.y, 20f);//SceneManager.LoadScene("home");//UnityEditor.EditorApplication.isPlaying = false;
        if (rb.position.z <= 5f) rb.position = new Vector3(rb.position.x, rb.position.y, 4200f);
        if (power > power_cap)
            power = power_cap;
        

        if ((carState == State.forward || carState == State.turnleft || carState == State.turnright) && Mathf.Abs(mag) < power_cap)
        {
            power = power * 1.2f;
            rb.AddRelativeForce(Vector3.forward * power);
            if (carState == State.turnleft)
            {
                rb.AddRelativeTorque(Vector3.up * turnPower * -1f);
            }
            else if (carState == State.turnright)
            {
                rb.AddRelativeTorque(Vector3.up * turnPower * 1f);
            }

        }
        else
        {
            if (power > carModifier)
                power = power * 0.7f;
        }

        turnPower = power * 0.9f;

        //Tried to keep it on the ground but still moving
        //rb.AddRelativeForce(Vector3.down * 0.1f * power);
    }
}
