using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class PickupEvent : CampaignEvent
    {
        string _item;

        public PickupEvent(string item, string grabbedBy) : base(grabbedBy)
        {
            _item = item;
        }

        public string Item
        {
            get
            {
                return _item;
            }
        }

        public string PickedUpBy
        {
            get
            {
                return Tag;
            }
        }
    }
}
