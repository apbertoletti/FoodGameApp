using FG.Domain;
using FG.Domain.Entities;
using FG.Domain.Enums;
using FG.Domain.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace FG.Tests.Services
{
    public class MenuTest
    {
        [Fact]
        public void Menu_Constructor_Test()
        {
            //Arrange

            //Act
            var menu = new Menu();

            //Assert
            menu.InitialQuestion.Should().NotBeNull();
            menu.InitialQuestion.Type.Should().Be(QuestionTypeEnum.Initial);

            menu.InitialQuestion.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Adjective);
            menu.InitialQuestion.NextQuestionYes.Description.Should().Be("Massa");
            menu.InitialQuestion.NextQuestionNo.Should().BeNull();

            menu.InitialQuestion.NextQuestionYes.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Food);
            menu.InitialQuestion.NextQuestionYes.NextQuestionYes.Description.Should().Be("Lasanha");
            menu.InitialQuestion.NextQuestionYes.NextQuestionYes.PreviousQuestion.Should().Be(menu.InitialQuestion.NextQuestionYes);

            menu.InitialQuestion.NextQuestionYes.NextQuestionNo.Type.Should().Be(QuestionTypeEnum.Food);
            menu.InitialQuestion.NextQuestionYes.NextQuestionNo.Description.Should().Be("Bolo de chocolate");
            menu.InitialQuestion.NextQuestionYes.NextQuestionNo.PreviousQuestion.Should().Be(menu.InitialQuestion.NextQuestionYes);
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Adjective);
            question.Description.Should().Be("Massa");
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Yes_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Food);
            question.Description.Should().Be("Lasanha");
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Yes_Yes_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Food);
            question.Description.Should().Be("Bolo de chocolate");
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_Yes_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_No_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.AskUser);
            question.Description.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Yes_No_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.AskUser);
            question.Description.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_No_TypeAnswer_Yes_No_No_Test()
        {
            //Arrange
            var menu = new Menu();
            var question = menu.InitialQuestion;

            //Act
            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);

            question = menu.AddQuestion(question, "Sorvete", "Gelado");

            question = menu.NextQuestion(question, AnswerTypeEnum.Yes);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);
            question = menu.NextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Adjective);
            question.Description.Should().Be("Gelado");
            
            question.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Food);
            question.NextQuestionYes.Description.Should().Be("Sorvete");
            
            question.NextQuestionYes.NextQuestionYes.Should().BeNull();
            question.NextQuestionYes.NextQuestionNo.Should().BeNull();
            
            question.NextQuestionNo.Should().BeNull();
            
            question.PreviousQuestion.Type.Should().Be(QuestionTypeEnum.AskUser);
            question.PreviousQuestion.Description.Should().BeNull();

        }


        [Fact]
        public void Menu_GetPreviousFoodDescription_AntecessorFood_Test()
        {
            //Arrange
            var menu = new Menu();

            var question1 = new Question()
            {
                Type = QuestionTypeEnum.Adjective,
                Description = "Doce",
            };

            var question2 = new Question()
            {
                Type = QuestionTypeEnum.Food,
                Description = "Goiabada",
                PreviousQuestion = question1
            };

            var question3 = new Question()
            {
                Type = QuestionTypeEnum.Adjective,
                Description = "Salgado",
                PreviousQuestion = question2
            };

            //Act
            var previewDescription = menu.GetPreviousFoodDescription(question3);

            //Assert
            previewDescription.Should().Be("Goiabada");
        }

        [Fact]
        public void Menu_GetPreviousFoodDescription_LastFood_Test()
        {
            //Arrange
            var menu = new Menu();

            var question1 = new Question()
            {
                Type = QuestionTypeEnum.Adjective,
                Description = "Salgado",
            };

            var question2 = new Question()
            {
                Type = QuestionTypeEnum.Food,
                Description = "Churrasco",
                PreviousQuestion = question1
            };

            //Act
            var previewDescription = menu.GetPreviousFoodDescription(question2);

            //Assert
            previewDescription.Should().Be("Churrasco");
        }
    }
}
