using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class WinTrigger : TriggerEvent
    {
        public WinTrigger()
        {
        }

        public CampaignMove CheckTrigger(CampaignEvent senderEvent)
        {   
            return CampaignMove.End;
        }
    }
}