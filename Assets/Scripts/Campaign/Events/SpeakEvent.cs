using UnityEngine;
using System.Collections;

namespace Campaign
{
    public enum SpeakerType { Mayor, ShopKeeper, Pedestrian, Villain };
    public class SpeakEvent : CampaignEvent
    {
        private int _speakerID;
        private SpeakerType _speakerType;
        private string _specialMessageId;

        public SpeakEvent(SpeakerType speakerType, int id):base(CampaignManager.PlayerTag)
        {
            _speakerID = id;
            _speakerType = speakerType;
            _specialMessageId = null;
        }

        public SpeakEvent(SpeakerType speakerType, int id, string specialMessageId) : base(CampaignManager.PlayerTag)
        {
            _speakerID = id;
            _speakerType = speakerType;
            _specialMessageId = specialMessageId;
        }

        public int SpeakerID
        {
            get
            {
                return _speakerID;
            }
        }

        public SpeakerType SpeakerType
        {
            get
            {
                return _speakerType;
            }
        }

        public string SpecialMessageId
        {
            get
            {
                return _specialMessageId;
            }
        }
    }
}