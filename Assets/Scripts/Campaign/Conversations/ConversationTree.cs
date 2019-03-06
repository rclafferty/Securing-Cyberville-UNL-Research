using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Campaign
{
    public class ConversationTree
    {
        string _speakerText;
        string[] _responses;
        ConversationTree[] _nextTrees;

        public ConversationTree(string speakerText, string[] responses, ConversationTree[] nextTrees)
        {
            if(responses.Length != nextTrees.Length)
            {
                throw new ArgumentException("Length of nextTrees must match length of responses");
            }
            _speakerText = speakerText;
            _responses = responses;
            _nextTrees = nextTrees;
        }

        public string SpeakerText
        {
            get
            {
                return _speakerText;
            }
        }

        public string[] Responses
        {
            get
            {
                return _responses;
            }
        }

        public ConversationTree[] NextTrees
        {
            get
            {
                return _nextTrees;
            }
        }

        public override string ToString()
        {
            return _speakerText;
        }
    }
}
