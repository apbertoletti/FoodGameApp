using FG.Domain.Entities.Base;
using FG.Domain.Enums;

namespace FG.Domain.Entities
{
    public class Question : Entity<Question>
    {
        public QuestionTypeEnum Type { get; set; }

        public string Description { get; set; }

        public Question NextQuestionYes { get; set; }

        public Question NextQuestionNo { get; set; }

        public Question PreviousQuestion { get; set; }
    }
}
