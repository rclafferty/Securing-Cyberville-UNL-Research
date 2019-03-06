using UnityEngine;
using System.Collections;

namespace Campaign
{
    public abstract class CampaignEvent
    {
        //Tag of the object that generated the event
        private string _tag;
        
        public CampaignEvent(string tag)
        {
            _tag = tag;
        }

        public string Tag
        {
            get
            {
                return _tag;
            }
        }
    }
}
