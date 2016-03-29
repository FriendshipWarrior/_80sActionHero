using UnityEngine;
using System.Collections;
using System;

namespace QuestSystem
{
    public class QuestIdentifier : IQuestIdentifier
    {
        private int sourceID;
        private int chainquestID;
        private int questID;

        public int ChainQuestID
        {
            get
            {
                return chainquestID;
            }
        }

        public int QuestID
        {
            get
            {
                return questID;
            }
        }

        public int SourceID
        {
            get
            {
                return sourceID;
            }
        }
    }
}
