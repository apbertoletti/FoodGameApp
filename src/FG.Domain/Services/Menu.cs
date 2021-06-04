using FG.Domain.Entities;
using FG.Domain.Enums;
using System.Collections.Generic;

namespace FG.Domain.Services
{
    public class Menu
    {
        public List<Question> Questions;
        
        public Question InitialQuestion { get; set; }

        public Menu()
        {
            InitialQuestion = new Question
            {
                Type = QuestionTypeEnum.Initial
            };
                      
            var firstQuestion = new Question 
            {
                Type = QuestionTypeEnum.Adjective,
                Description = "Massa"               
            };

            InitialQuestion.NextQuestionYes = firstQuestion;

            var firstYesQuestion = new Question
            {
                Type = QuestionTypeEnum.Food,
                Description = "Lasanha",
            };

            firstQuestion.NextQuestionYes = firstYesQuestion;

            var firstNoQuestion = new Question
            {
                Type = QuestionTypeEnum.Food,
                Description = "Bolo de chocolate",                
            };

            firstQuestion.NextQuestionNo = firstNoQuestion;
        }

        public Question NextQuestion(Question question, AnswerTypeEnum answerType)
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
