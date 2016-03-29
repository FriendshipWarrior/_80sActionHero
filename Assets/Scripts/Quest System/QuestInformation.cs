using System;

namespace QuestSystem
{
    public class QuestInformation : IQuestInformation
    {
        private string name;
        private string description;
        private string hint;
        private string dialogue;

        public string Description
        {
            get
            {
                return description;
            }
        }

        public string Dialog
        {
            get
            {
                return dialogue;
            }
        }

        public string Hint
        {
            get
            {
                return hint;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
