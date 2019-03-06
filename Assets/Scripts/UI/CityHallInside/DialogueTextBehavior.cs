using UnityEngine;
using UnityEngine.UI;

public class DialogueTextBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Mayor").GetComponent<MayorOfficeDialogue>().AssignDialogue(this.GetComponent<Text>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
