using UnityEngine;

using System.Text;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

using UnityEngine.SceneManagement;
//using System.Data;
//using System.Data.Odbc;

//using MySql.Data.MySqlClient;
namespace Assets.Scripts.UserSessionLogger
{
    public class UserSessionLoggerBehavior : MonoBehaviour
    {
        WWWForm form;

        GameObject player;

        private string _player;
        private string _date;
        private string _comment;
        private UserAction _action;
        private string _scene;

        public enum UserAction
        {
            LeftClickBlankSpace,
            LeftClickButton, 
            LeftClickLink,
            MiddleClick,
            RightClickBlankSpace,
            RightClickButton,
            RightClickLink,
            ScrollDown,
            ScrollUp,
            PickupItem,
            UsedItem,
            SceneChange,
            TEST,
            Setup
        };
        
        void Start()
        {
            player = GameObject.Find("Player");
            DontDestroyOnLoad(this);
        }

        /*void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        //Left button click
                        uslb.LogToDatabase("Left button clicked at (" + Input.mousePosition.x + "," + Input.mousePosition.y + ")", UserAction.LeftClickBlankSpace);
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        //Right button click
                        uslb.LogToDatabase("Right button clicked at (" + Input.mousePosition.x + "," + Input.mousePosition.y + ")", UserAction.RightClickBlankSpace);
                    }
                    else if (Input.GetMouseButtonDown(2))
                    {
                        //Middle button click
                        uslb.LogToDatabase("Middle button clicked at (" + Input.mousePosition.x + "," + Input.mousePosition.y + ")", UserAction.RightClickBlankSpace);
                    }
                }
            }
        }*/

        /// <summary>
        /// Logs comment to file userlogger.txt by default and appends to existing file
        /// </summary>
        /// <param name="comment"></param>
        public void LogToFile(string comment)
        {
            LogToFile(comment, "userlogger.txt", true);
        }

        /// <summary>
        /// Logs comment to given file
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="filepath"></param>
        /// <param name="a"></param>
        public void LogToFile(string comment, string filepath, bool a)
        {
            string user = player.GetComponent<PlayerControls>().Username;
            string date = DateTime.Now.ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append(date);
            sb.Append(" - ");
            sb.Append(user);
            sb.Append(" - ");
            sb.Append(comment);

            using (StreamWriter sw = new StreamWriter(filepath, append: a))
            {
                sw.WriteLine(sb.ToString());
            }
        }

        public void NewLogStart()
        {
            using (StreamWriter sw = new StreamWriter("userlogger.txt", append: true))
            {
                sw.WriteLine("-----");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator LogDatabase(ArrayList parameters)
        {
            //Inspired heavily by the Udemy tutorial in my logs

            form = new WWWForm();
            form.AddField("user", (string)parameters[0]);
            form.AddField("scene", (string)parameters[1]);
            form.AddField("date", (string)parameters[2]);
            form.AddField("comment", (string)parameters[3]);
            form.AddField("action", (int)parameters[4] + 1);
            //Debug.Log((int)parameters[4]);

            WWW w = new WWW("http://people.cs.ksu.edu/~rclafferty/securing_cyberville/log.php", form);
            yield return w;

            string text = w.text.ToLower();
            Debug.Log(text);
            if (string.IsNullOrEmpty(w.error)) //No error
            {
                if (text.Contains("error while inserting log")
                    || text.Contains("notice"))
                {
                    Debug.Log("An error occurred");
                }
                else
                {
                    //All is well
                }
            }
            else
            {
                Debug.Log("An error occurred");
            }
        }

        public void LogToDatabase(string comment, UserAction action)
        {
            ArrayList a = new ArrayList();
            string user = player.GetComponent<PlayerControls>().Username;
            a.Add(user);
            a.Add(SceneManager.GetActiveScene().name);
            DateTime now = DateTime.Now;
            StringBuilder date = new StringBuilder();
            date.Append(now.Year.ToString("0000"));
            date.Append("-");
            date.Append(now.Month.ToString("00"));
            date.Append("-");
            date.Append(now.Day.ToString("00"));
            date.Append(" ");
            date.Append(now.Hour.ToString("00"));
            date.Append(":");
            date.Append(now.Minute.ToString("00"));
            date.Append(":");
            date.Append(now.Second.ToString("00"));
            a.Add(date.ToString());
            //a.Add(DateTime.Now.ToString("yyyy-MM-ss HH:mm:ss"));
            a.Add(comment);
            a.Add(action);

            StartCoroutine("LogDatabase", a);
        }
    }
}