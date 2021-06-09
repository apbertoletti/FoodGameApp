using FG.Domain.Entities;
using FG.Domain.Enums;

namespace FG.Domain.Interfaces.Services
{
    public interface IMenuService
    {
        Question GetNextQuestion(Question question, AnswerTypeEnum answerType);

        Question AddQuestion(Question actualQuestion, string foodNameChoosed, string foodAdjectiveChoosed);

        string GetPreviousFoodDescription(Question actualQuestion);

    }
}
