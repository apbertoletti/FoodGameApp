using System;
using System.Collections.Generic;

namespace FG.Domain
{
    public class Menu
    {
        public List<Question> Questions;
        
        public Question FirstQuestion { get; set; }

        public Menu()
        {
            FirstQuestion = new Question 
            {
                Type = QuestionTypeEnum.Adjective,
                Description = "Massa",
                NextQuestionYes = new Question
                {
                    Type = QuestionTypeEnum.Food,
                    Description = "Lasanha",
                },
                NextQuestionNo = new Question
                {
                    Type = QuestionTypeEnum.Answer,
                },
            };
        }

        public Question NextQuestion(Question question, AnswerTypeEnum answerType)
        {
            return (answerType == AnswerTypeEnum.Yes ? question.NextQuestionYes : question.NextQuestionNo);
        }

        public void Answer(Question question, AnswerTypeEnum answerType, Question nextQuestion)
        { 
        }
    }
}
