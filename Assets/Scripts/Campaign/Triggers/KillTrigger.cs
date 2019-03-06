using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class KillTrigger : TriggerEvent
    {
        private string _targetTag;
        private int _count;

        public KillTrigger(string target, int count)
        {
            _targetTag = target;
            _count = count;
        }

        public CampaignMove CheckTrigger(CampaignEvent senderEvent)
        {
            KillEvent kill;
            if(senderEvent is KillEvent)
            {
                kill = (KillEvent)senderEvent;
                if(kill.Killer == CampaignManager.PlayerTag && kill.Killed == _targetTag)
                {
                    _count--;
                }
                if (_count < -0)
                {
                    return CampaignMove.Success;
                }
            }
            
            return CampaignMove.None;
        }
    }
}