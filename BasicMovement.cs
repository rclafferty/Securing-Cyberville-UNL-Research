using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    float dead_value;
    float neg_dead_value;
    Rigidbody rb;

    [SerializeField]
    float power;
    [SerializeField]
    float turnPower;
    [SerializeField]
    float carModifier;

    // Use this for initialization
    void Start () {
        dead_value = 0.1f;
        neg_dead_value = 0 - dead_value;
        rb = this.GetComponent<Rigidbody>();

        carModifier = 5f;
        power = carModifier * 100f;
        turnPower = power * 2f;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical") > dead_value)
        {
            //rb.MovePosition(rb.position + Vector3.forward);
            rb.AddRelativeForce(Vector3.forward * power);
            if (Input.GetAxisRaw("Mouse X") > dead_value || Input.GetAxisRaw("Horizontal") > dead_value)
            {
                //rb.MovePosition(rb.position + Vector3.forward);
                rb.AddRelativeTorque(Vector3.up * turnPower);
            }
            if (Input.GetAxisRaw("Mouse X") < neg_dead_value || Input.GetAxisRaw("Horizontal") < neg_dead_value)
            {
                //rb.MovePosition(rb.position + Vector3.forward);
                rb.AddRelativeTorque(Vector3.down * turnPower);
            }
        }
        if (Input.GetAxisRaw("Vertical") < neg_dead_value)
        {
            //rb.MovePosition(rb.position + Vector3.forward);
            rb.AddRelativeForce(Vector3.back * power);
            if (Input.GetAxisRaw("Mouse X") > dead_value || Input.GetAxisRaw("Horizontal") > dead_value)
            {
                //rb.MovePosition(rb.position + Vector3.forward);
                rb.AddRelativeTorque(Vector3.down * turnPower);
            }
            if (Input.GetAxisRaw("Mouse X") < neg_dead_value || Input.GetAxisRaw("Horizontal") < neg_dead_value)
            {
                //rb.MovePosition(rb.position + Vector3.forward);
                rb.AddRelativeTorque(Vector3.up * turnPower);
            }
        }

        if (Input.GetAxis("Break") > dead_value)
        {
            if (rb.velocity.x > 0f)
            {
                rb.AddRelativeForce(Vector3.left * power);
            }
            if (rb.velocity.x < 0f)
            {
                rb.AddRelativeForce(Vector3.right * power);
            }
            if (rb.velocity.z > 0f)
            {
                rb.AddRelativeForce(Vector3.back * power);
            }
            if (rb.velocity.z < 0f)
            {
                rb.AddRelativeForce(Vector3.forward * power);
            }
        }

        //Tried to keep it on the ground but still movingw
        //rb.AddRelativeForce(Vector3.down * 0.1f * power);
    }
}
