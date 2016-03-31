using UnityEngine;
using System.Collections;

namespace QuestSystem
{
    public interface IQuestIdentifier
    {
        int SourceID { get; }
        int ChainQuestID { get; }
        int QuestID { get; }
    }
}
