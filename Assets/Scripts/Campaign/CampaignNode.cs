using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class CampaignNode
    {
        CampaignNode _success;
        CampaignNode _failure;
        MissionInitializer _init;
        TriggerEvent _trigger;
        int _id;
        string _description;
        
        /// <summary>
        /// Contructor for a new CampaignNode where the mission does not have an initializer
        /// </summary>
        /// <param name="success">CampaignNode to move to on successful mission completion</param>
        /// <param name="failure">CampaignNode to move to on mission failure</param>
        /// <param name="trigger">Trigger to determine if/where to move in the campaign tree</param>
        /// <param name="id">Identifier for the new CampaignNode</param>
        public CampaignNode(CampaignNode success, CampaignNode failure, TriggerEvent trigger, int id, string description)
        {
            _success = success;
            _failure = failure;
            _trigger = trigger;
            _id = id;
            _init = null;
            _description = description;
        }

        /// <summary>
        /// Constructor for a new Campaign Node, complete with an initializer for the mission.
        /// </summary>
        /// <param name="success">CampaignNode to move to on successful mission completion</param>
        /// <param name="failure">CampaignNode to move to on mission failure</param>
        /// <param name="trigger">TriggerEvent to determine if/where to move in the campaign tree</param>
        /// <param name="id">Identifier for the new CampaignNode</param>
        /// <param name="init">MissionInitializer used to initialize things for the mission</param>
        public CampaignNode(CampaignNode success, CampaignNode failure, TriggerEvent trigger, int id, string description, MissionInitializer init)
        {
            _success = success;
            _failure = failure;
            _trigger = trigger;
            _id = id;
            _init = init;
            _description = description;
        }

        /// <summary>
        /// Returns the node in the campaign to move to on a success.
        /// </summary>
        public CampaignNode Success
        {
            get
            {
                return _success;
            }
        }

        /// <summary>
        /// Returns the node in the campaign tree to move to upon a mission failure.
        /// </summary>
        public CampaignNode Failure
        {
            get
            {
                return _failure;
            }
        }

        /// <summary>
        /// Returns the trigger to determine where to move in the campaign tree.
        /// </summary>
        public TriggerEvent Trigger
        {
            get
            {
                return _trigger;
            }
        }

        /// <summary>
        /// Returns the ID of the current CampaignNode.
        /// </summary>
        public int ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// Returns the initializer for the current mission in the campaign.
        /// </summary>
        public MissionInitializer Initializer
        {
            get
            {
                return _init;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
        }

        public override string ToString()
        {
            return _description;
        }
    }
}
