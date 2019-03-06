using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaign
{
    class LinkedListCell<t,u>
    {
        t _data1;
        u _data2;
        LinkedListCell<t,u> _next;

        public LinkedListCell(t data1, u data2)
        {
            _data1 = data1;
            _data2 = data2;
        }

        public LinkedListCell<t,u> Next
        {
            get
            {
                return _next;
            }
            set
            {
                _next = value;
            }
        }

        public t Data1
        {
            get
            {
                return _data1;
            }
        }

        public u Data2
        {
            get
            {
                return _data2;
            }
        }
    }

    class ConversationManager
    {
        //LinkedList bonanza, which store which conversationTree to use based on (in order) speakerType, speakerID, CampaignId
        LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>> conversations;

        //Array of default conversations based on SpeakerType
        ConversationTree[] defaults;

        public ConversationManager()
        {
            defaults = ConversationInitializer.InitializeConversationDefaults();
            conversations = ConversationInitializer.InitializeConversations();
        }

        public ConversationTree GetConversation(SpeakerType type, int speakerId, int campaignId)
        {
            ConversationTree result;
            LinkedListCell<SpeakerType, LinkedListCell<int, LinkedListCell<int, ConversationTree>>> temp = conversations;

            while(temp != null)
            {
                if(temp.Data1 == type)
                {
                    result = GetSpeakerConvo(temp.Data2, speakerId, campaignId);
                    if (result != null)
                    {
                        return result;
                    }
                }
                temp = temp.Next;
            }
            return defaults[(int)type];
        }

        private ConversationTree GetSpeakerConvo(LinkedListCell<int, LinkedListCell<int, ConversationTree>>current, int speakerId, int campaignId)
        {
            ConversationTree result;
            LinkedListCell<int, LinkedListCell<int, ConversationTree>> temp = current;
            while (temp != null)
            {
                if(temp.Data1 == speakerId)
                {
                    result = GetCampaignConvo(temp.Data2, campaignId);
                    if(result != null)
                    {
                        return result;
                    }
                }
                temp = temp.Next;
            }
            return null;
        }

        private ConversationTree GetCampaignConvo(LinkedListCell<int, ConversationTree> current, int campaignId)
        {
            LinkedListCell<int, ConversationTree> temp = current;
            while (temp != null)
            {
                if(temp.Data1 == campaignId)
                {
                    return temp.Data2;
                }
                temp = temp.Next;
            }
            return null;
        }
    }
}
