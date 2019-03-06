using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCameraSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera.main.GetComponent<CameraMovement>().SetStationary(new Vector3(0, 95, 563));
        Camera.main.GetComponent<CameraMovement>().IsDriving(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
