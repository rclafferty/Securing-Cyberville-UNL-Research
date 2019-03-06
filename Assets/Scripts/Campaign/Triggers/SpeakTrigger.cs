using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class SpeakTrigger : TriggerEvent
    {
        private SpeakerType _speakerType;
        private int _speakerId;
        private string _specialMessageId;

        public SpeakTrigger(SpeakerType type, int id, string special)
        {
            _speakerType = type;
            _speakerId = id;
            _specialMessageId = special;
        }

        public SpeakTrigger(SpeakerType type, int id)
        {
            _speakerType = type;
            _speakerId = id;
            _specialMessageId = null;
        }

        public CampaignMove CheckTrigger(CampaignEvent senderEvent)
        {
            SpeakEvent speak;
            Debug.Log("Checking Event");
            if(senderEvent is SpeakEvent)
            {
                Debug.Log("Event is SpeakEvent");
                speak = (SpeakEvent)senderEvent;
                if(_specialMessageId == null)
                {
                    Debug.Log("Special ID: '" + _specialMessageId + "' compared to '" + speak.SpecialMessageId + "'");
                    if((speak.SpeakerType == _speakerType)&&(speak.SpeakerID == _speakerId))
                    {
                        return CampaignMove.Success;
                    }
                }
                else
                {
                    if ((speak.SpecialMessageId != null) && (speak.SpeakerType == _speakerType) && (speak.SpeakerID == _speakerId) && (speak.SpecialMessageId == _specialMessageId))
                    {
                        return CampaignMove.Success;
                    }
                }
            }
            
            return CampaignMove.None;
        }
    }
}