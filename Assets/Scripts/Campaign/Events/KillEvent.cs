using UnityEngine;
using System.Collections;

namespace Campaign
{
    public class KillEvent : CampaignEvent
    {
        private string _killed;

        public KillEvent(string killed, string killer):base(killer)
        {
            _killed = killed;
        }

        public string Killed
        {
            get
            {
                return _killed;
            }
        }

        public string Killer
        {
            get
            {
                return Tag;
            }
        }
    }
}