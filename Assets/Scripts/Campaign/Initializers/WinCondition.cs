using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class WinCondition : MissionInitializer
    {
        public WinCondition() {  }

        public bool InitializeMission()
        {
            //Change to Win Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("end");
            return true;
        }
    }
}