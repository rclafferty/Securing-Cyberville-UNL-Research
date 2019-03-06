using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class PickupTrigger : TriggerEvent
    {
        private string _item;
        private int _count;

        public PickupTrigger(string item, int count)
        {
            _item = item;
            _count = count;
        }

        public CampaignMove CheckTrigger(CampaignEvent triggeringEvent)
        {
            PickupEvent trigger;
            if(triggeringEvent is PickupEvent)
            {
                trigger = (PickupEvent)triggeringEvent;
                if (trigger.Item == _item)// && trigger.PickedUpBy == CampaignManager.PlayerTag)
                {
                    _count--;
                }
                if(_count <= 0)
                {
                    return CampaignMove.Success;
                }
            }
            return CampaignMove.None;
        }
    }
}
