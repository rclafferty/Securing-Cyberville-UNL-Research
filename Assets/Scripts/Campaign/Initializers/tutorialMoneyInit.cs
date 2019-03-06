using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Testing;
using Assets.Scripts.Driving;
namespace Campaign
{
    public class TutorialMoneyInitialier : MonoBehaviour, MissionInitializer
    {
        Car player;// = GameObject.Find(CampaignManager.PlayerTag).GetComponent<PlayerControls>().GetCarObject();
        public Transform Money;
        public TutorialMoneyInitialier() {
             player = GameObject.Find(CampaignManager.PlayerTag).GetComponent<PlayerControls>().GetCarObject();
        }

        public bool InitializeMission()
        {
            //Debug.Log(SceneManager.GetActiveScene().name);
            Vector3 position = player.GetPosition();
            //Spawn 10 Money for 

            /*for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.AddComponent<Rigidbody>();
                    cube.transform.position = new Vector3(x, y, 0);
                }
            }*/

            Money = GameObject.Find("Money").transform;

            /*for (var i = 0; i<1; i += 12)
            {
                for(var j = 0; j<1; j += 12)
                {
                    Instantiate(Money, new Vector3(i, 0, j), Quaternion.identity);
                }
            }*/
            //for (int i = 1; i < 4; i++)

            Instantiate(Money, new Vector3(Random.Range(-1000, 1000), 3, -1500 + Random.Range(-1000, 1000)), Quaternion.identity);
            Instantiate(Money, new Vector3(Random.Range(-1000, 1000), 3, -1500 + Random.Range(-1000, 1000)), Quaternion.identity);
            Instantiate(Money, new Vector3(Random.Range(-1000, 1000), 3, -1500 + Random.Range(-1000, 1000)), Quaternion.identity);
            Instantiate(Money, new Vector3(Random.Range(-1000, 1000), 3, -1500 + Random.Range(-1000, 1000)), Quaternion.identity);
            Instantiate(Money, new Vector3(Random.Range(-1000, 1000), 3, -1500 + Random.Range(-1000, 1000)), Quaternion.identity);
            return true;
        }
    }
}