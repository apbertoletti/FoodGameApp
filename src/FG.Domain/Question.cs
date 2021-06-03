namespace FG.Domain
{
    public class Question
    {
        public QuestionTypeEnum Type { get; set; }
        
        public string Description { get; set; }

        public Question NextQuestionYes { get; set; }

        public Question NextQuestionNo { get; set; }
    }
}
