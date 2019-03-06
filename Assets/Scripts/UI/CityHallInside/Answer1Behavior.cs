using UnityEngine;
using UnityEngine.UI;

public class Answer1Behavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Mayor").GetComponent<MayorOfficeDialogue>().AssignAnswer1(this.GetComponent<Button>(), this.GetComponent<Text>());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
