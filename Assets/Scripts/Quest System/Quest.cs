using System.Collections.Generic;

namespace QuestSystem
{
    public class Quest
    {
        //Name
        //Description
        //Hint
        //Quest Dialog
        //srouceID
        //questID
        //chain quest and the next quest is blank
        //chainquestid
        public Quest()
        {

        }


        //objectives
        private List<IQuestObjective> objectives;
        //collection objective
        //1 vhs
        //1 boss
        //location objective
        //go from a to b
        //timed
        //bonus objectives
        //rewards
        //events
        //on completetion event
        //on failed
        //on update

        private bool IsComplete()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                if (objectives[i].IsComplete == false && objectives[i].IsBonus == false)
                {
                    return false;
                }
            }
            return true;    //get reward!! fire on complete event!
        }
    }
}

