using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.IO;

using Assets.Scripts;
using Assets.Scripts.Driving;
using Assets.Scripts.Testing;

public class SaveState : MonoBehaviour
{
    public string path = "savestate.txt";

    void Start ()
    {

    }

    void Update ()
    {

    }

    public void Save ()
    {
        Debug.Log("Saving...");
        using (StreamWriter sw = new StreamWriter(path))
        {
            GameObject player = GameObject.Find("Player");
            string sceneName = SceneManager.GetActiveScene().name;
            PlayerControls pc = player.GetComponent<PlayerControls>();
            Backpack b = pc.GetBackpack();
            ArrayList a = b.GetBackpack();
            
            sw.WriteLine(sceneName);
            sw.WriteLine(pc.GetCarObject().GetRigidBody().transform.position.ToString());
            sw.WriteLine(a.Count);
            for (int i = 0; i < a.Count; i++)
                sw.WriteLine(((Item)a[i]).id + " " + ((Item)a[i]).quantity);

            Debug.Log("FINISH S");
        }

        Debug.Log("Finished saving");
    }

    public void Load ()
    {
        using (StreamReader sr = new StreamReader(path))
        {
            PlayerControls pc = GameObject.Find("Player").GetComponent<PlayerControls>();
            Backpack b = pc.GetBackpack();
            string name = "";
            ArrayList backpack = new ArrayList();

            name = sr.ReadLine();
            //Debug.Log("Scene = " + name);
            string coor = sr.ReadLine();
            //Debug.Log("coor = " + coor);
            int x = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < x; i++)
            {
                string s = sr.ReadLine();
                //Debug.Log("item = " + s);
                string[] st = s.Split(' ');
                backpack.Add(new Item(Convert.ToInt32(st[0]), Convert.ToInt32(st[1])));
            }

            string[] c = coor.Split('(');
            /*Debug.Log("c len is " + c.Length);
            for (int i = 0; i < c.Length; i++)
                Debug.Log("C = " + c[i]);*/
            c = c[1].Split(')');
            /*Debug.Log("c len is " + c.Length);
            for (int i = 0; i < c.Length; i++)
                Debug.Log("C = " + c[i]);*/
            c = c[0].Split(',');
            /*Debug.Log("c len is " + c.Length);
            for (int i = 0; i < c.Length; i++)
                Debug.Log("C = " + c[i]);*/
            Vector3 v = new Vector3((float)Convert.ToDouble(c[0].Trim()), (float)Convert.ToDouble(c[1].Trim()), (float)Convert.ToDouble(c[2].Trim()));
            pc.transform.position = v;
            b.CheatSetBackpack(backpack);

            SceneManager.LoadScene(name);
        }
    }
}
