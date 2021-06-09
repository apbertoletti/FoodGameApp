using FG.Domain.Entities;
using FG.Domain.Enums;
using FG.Domain.Interfaces.Services;

namespace FG.Domain.Services
{
    public class MenuService : IMenuService
    {
        private IQuestionService _questionService;

        public Question InitialQuestion { get; private set; }

        public MenuService(IQuestionService questionService)
        {
            _questionService = questionService;

            InitialQuestion = _questionService.GetInitialQuestion();
        }

        public Question GetNextQuestion(Question question, AnswerTypeEnum answerType)
        {
            if (answerType == AnswerTypeEnum.No)
            {
                if (question.NextQuestionNo is null)
                {
                    return new Question
                    {
                        Type = QuestionTypeEnum.AskUser,
                        PreviousQuestion = question
                    };
                }
            }

            return (answerType == AnswerTypeEnum.Yes ? question.NextQuestionYes : question.NextQuestionNo);
        }

        public Question AddQuestion(Question actualQuestion, string foodNameChoosed, string foodAdjectiveChoosed)
        {
            var newQuestionNo = new Question
            {
                Type = QuestionTypeEnum.Adjective,
                Description = foodAdjectiveChoosed,
                PreviousQuestion = actualQuestion
            };

            newQuestionNo.NextQuestionYes = new Question
            {
                Type = QuestionTypeEnum.Food,
                Description = foodNameChoosed,
                PreviousQuestion = newQuestionNo
            };

            actualQuestion.PreviousQuestion.NextQuestionNo = newQuestionNo;

            return InitialQuestion;
        }

        public string GetPreviousFoodDescription(Question actualQuestion)
        {
            if (actualQuestion.Type == QuestionTypeEnum.Food)
                return actualQuestion.Description;

            return GetPreviousFoodDescription(actualQuestion.PreviousQuestion);
        }
    }
}
