using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Campaign;

namespace Campaign
{
    public enum InfectionType { virus, worm };

    public class CampaignManager : MonoBehaviour
    {
        //Tag for the player character in Unity
        public static string PlayerTag = "Player";
        
        //Defines the number of infection types (should correspond to the Infection enum)
        private const int InfectionTypeCount = 10;

        //Defines the maximum number of scenes
        private const int WorldCount = 32;

        //Defines the maximum infection level possible for any given scene
        private const int MaxInfectionLevel = 255;

        //Defines the thresholds for low, medium and high infection levels
        private const int HighInfectionThreshold = 150;
        private const int MediumInfectionThreshold = 75;
        private const int LowInfectionThreshold = 25;

        //Array to store the types and levels of infection for each world
        private int[,] _infectionStatus;

        //Current node in Campaign
        CampaignNode currentMission;

        //Conversation Manager to store and handle all conversations
        ConversationManager convoManager;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }


        // Use this for initialization
        void Start()
        {
            _infectionStatus = new int[InfectionTypeCount, WorldCount];
            currentMission = CampaignInitializer.InitializeCampaign();
            //Debug.Log(currentMission.ToString());
            if(currentMission.Initializer != null)
            {
                currentMission.Initializer.InitializeMission();
            }
            convoManager = new ConversationManager();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// InfectionLevel returns an array representing the levels of the
        /// various infection types for the given world index.
        /// </summary>
        /// <param name="world">World index to return infection statistics for</param>
        /// <returns>An int[] representing the various levels of infection</returns>
        public int[] InfectionLevel(int world)
        {
            int[] result = new int[InfectionTypeCount];
            for (int i = 0; i < InfectionTypeCount; i++)
            {
                result[i] = _infectionStatus[i, world];
            }
            return result;
        }

        /// <summary>
        /// Method used to increment the infection level of a specific infection type for a given world.
        /// </summary>
        /// <param name="type">Type of infection to increment</param>
        /// <param name="world">World to increment the infection level in</param>
        /// <param name="amount">Amount to increase the infection level</param>
        /// <returns>New infection level of the given infection type</returns>
        public int IncrementInfection(InfectionType type, int world, int amount)
        {
            return _infectionStatus[(int)type, world] += amount;
        }

        /// <summary>
        /// Method used to decrement the infection level of a specific infectioin type for a given world.
        /// </summary>
        /// <param name="type">Type of infection to decrement</param>
        /// <param name="world">World to decrement the infection level in</param>
        /// <param name="amount">Amount to decrease the infection level</param>
        /// <returns>New infection level of the given infection type</returns>
        public int DecrementInfection(InfectionType type, int world, int amount)
        {
            return _infectionStatus[(int)type, world] -= amount;
        }

        /// <summary>
        /// Method to check a campaign event to see if the player has completed a campaign mission and
        /// needs to progress through the campaign tree.
        /// </summary>
        /// <param name="evnt">Campaign event to check</param>
        public void CheckEvent(CampaignEvent evnt)
        {
            CampaignMove result = currentMission.Trigger.CheckTrigger(evnt);
            if (result == CampaignMove.None)
            {
                return;
            }
            else if (result == CampaignMove.Success)
            {
                currentMission = currentMission.Success;
                if (currentMission.Initializer != null)
                {
                    currentMission.Initializer.InitializeMission();
                }
            }
            else if (result == CampaignMove.Failure)
            {
                currentMission = currentMission.Failure;
                if (currentMission.Initializer != null)
                {
                    currentMission.Initializer.InitializeMission();
                }
            }
            else if (result == CampaignMove.End)
            {
                //CHANGE TO END OF GAME SCENE
                UnityEngine.SceneManagement.SceneManager.LoadScene("end");
            }
            else
            {
                throw new ArgumentException("Invalid response from CheckTrigger on Node " + currentMission.ID);
            }

            Debug.Log("CampaignEvent = " + currentMission.ToString());
        }

        public ConversationTree GetConversation(SpeakerType type, int speakerId)
        {
            return convoManager.GetConversation(type, speakerId, currentMission.ID);
        }
    }
}
