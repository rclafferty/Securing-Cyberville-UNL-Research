using UnityEngine;
using System.Collections;

namespace Campaign
{
    public enum CampaignMove { None, Success, Failure, End};

    public interface TriggerEvent
    {
        CampaignMove CheckTrigger(CampaignEvent campaignEvent);
    }
}
