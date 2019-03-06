using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControllerBehavior : MonoBehaviour {

    GameObject accept;
    GameObject clear;
    GameObject cancel;

    Button[,] items;
    [SerializeField]
    int x;
    [SerializeField]
    int y;

    float currVal;
    float currValHorizontal;
    float prevVal;
    float prevValHorizontal;
    float dead;
    float neg_dead;

    int maxX;
    int maxY;

	// Use this for initialization
	void Start () {
        dead = 0.8f;
        neg_dead = 0 - dead;
        accept = GameObject.Find("AcceptButton");
        clear = GameObject.Find("ClearButton");
        cancel = GameObject.Find("CancelButton");

        int len = 4;
        string[] it = new string[len];
        it[0] = "Overclock";
        it[1] = "VirusBeGone";
        it[2] = "WormBeGone";
        it[3] = "Invincibility";

        x = 0;
        y = 0;
        items = new Button[5,4];
        for (int i = 0; i < len; i++)
        {
            string s = it[i];
            items[i, 0] = f(s + "Plus");
            items[i, 1] = f(s + "Minus");
            items[i, 2] = f(s + "SellPlus");
            items[i, 3] = f(s + "SellMinus");
        }

        items[4, 0] = accept.GetComponent<Button>();
        items[4, 1] = clear.GetComponent<Button>();
        items[4, 2] = cancel.GetComponent<Button>();

        maxX = 4;
        maxY = 3;
    }
	
	// Update is called once per frame
	void Update () {
        currVal = Input.GetAxisRaw("Vertical");
        //currVal = Mathf.Round(currVal * 100f) / 100f;
        currValHorizontal = Input.GetAxisRaw("Horizontal");
        if (currVal < neg_dead) //down
        {
            if (prevVal < neg_dead)
                return;

            x++;
            if (x > maxX)
                x = maxX;

            if (x == maxX && y == maxY)
            {
                y--;
            }
        }
        else if (currVal > dead) //up
        {
            if (prevVal > dead)
                return;

            x--;
            if (x < 0)
                x = 0;
        }
        else if (currValHorizontal > dead) //right
        {
            if (prevValHorizontal > dead)
                return;

            y++;
            if (y > maxY)
                y = maxY;

            if (x == maxX && y == maxY)
            {
                y--;
            }
        }
        else if (currValHorizontal < neg_dead) //left
        {
            if (prevValHorizontal < neg_dead)
                return;

            y--;
            if (y < 0)
                y = 0;
        }
        s(x, y);
        prevVal = currVal;
        prevValHorizontal = currValHorizontal;
    }

    Button f(string s)
    {
        return GameObject.Find(s).GetComponent<Button>();
    }

    void s(int x, int y)
    {
        items[x, y].Select();
    }
}
