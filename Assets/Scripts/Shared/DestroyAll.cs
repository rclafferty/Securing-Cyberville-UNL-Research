using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("CampaignManager"));
        //Destroy(GameObject.Find("Money"));
        GameObject[] g = GameObject.FindGameObjectsWithTag("Money");
        for (int i = 0; i < g.Length; i++)
        {
            Destroy(g[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
