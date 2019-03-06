using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    int[] randomCoins;

	// Use this for initialization
	void Start () {
        int x = 100;
        randomCoins = new int[x];
        for (int i = 0; i < x; i++)
            randomCoins[i] = 10;
        randomCoins[72] = 200;
        randomCoins[56] = 200;
        randomCoins[63] = 200;
        randomCoins[34] = 500;
        randomCoins[82] = 1000;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision c)
    {
        /*Debug.Log("COLLIDED");

        int x = 1;
        if (this.name == "Money")
            x = 500;

        GameObject.Find("Player").GetComponent<PlayerControls>().PickupItem(this.name, x);
        this.gameObject.SetActive(false);

        Debug.Log("PICKED UP " + this.name);*/
    }

    void OnTriggerEnter (Collider c)
    {
        //Debug.Log("COLLIDED");

        int x = 1;
        if (this.name == "Money" || this.name == "Money(Clone)")
            x = randomCoins[Random.Range(0,100)];

        GameObject.Find("Player").GetComponent<PlayerControls>().PickupItem(this.name, x);
        this.gameObject.SetActive(false);

        //Debug.Log("PICKED UP " + this.name);
    }
}
