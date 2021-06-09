using FG.Domain.Entities;
using FG.Domain.Enums;
using FG.Domain.Interfaces.Services;

namespace FG.Domain.Services
{
    public class QuestionService : IQuestionService
    {
        public Question GetInitialQuestion()
        {
            var initialQuestion = new Question
            {
                Type = QuestionTypeEnum.Initial
            };

            var firstQuestion = new Question
            {
                Type = QuestionTypeEnum.Adjective,
                Description = "Massa"
            };

            initialQuestion.NextQuestionYes = firstQuestion;
            firstQuestion.PreviousQuestion = initialQuestion;

            var firstYesQuestion = new Question
            {
                Type = QuestionTypeEnum.Food,
                Description = "Lasanha",
            };

            firstQuestion.NextQuestionYes = firstYesQuestion;
            firstYesQuestion.PreviousQuestion = firstQuestion;

            var firstNoQuestion = new Question
            {
                Type = QuestionTypeEnum.Food,
                Description = "Bolo de chocolate",
            };

            firstQuestion.NextQuestionNo = firstNoQuestion;
            firstNoQuestion.PreviousQuestion = firstQuestion;

            return initialQuestion;
        }
    }
}
