using FG.Domain.Entities;
using FG.Domain.Enums;
using FG.Domain.Services;
using FluentAssertions;
using Xunit;

namespace FG.Tests.Services
{
    public class MenuServiceTest
    {
        [Fact]
        public void Menu_Constructor_Test()
        {
            //Arrange
            var questionService = new QuestionService();

            //Act
            var menuService = new MenuService(questionService);

            //Assert
            menuService.InitialQuestion.Should().NotBeNull();
            menuService.InitialQuestion.Type.Should().Be(QuestionTypeEnum.Initial);

            menuService.InitialQuestion.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Adjective);
            menuService.InitialQuestion.NextQuestionYes.Description.Should().Be("Massa");
            menuService.InitialQuestion.NextQuestionNo.Should().BeNull();

            menuService.InitialQuestion.NextQuestionYes.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Food);
            menuService.InitialQuestion.NextQuestionYes.NextQuestionYes.Description.Should().Be("Lasanha");
            menuService.InitialQuestion.NextQuestionYes.NextQuestionYes.PreviousQuestion.Should().Be(menuService.InitialQuestion.NextQuestionYes);

            menuService.InitialQuestion.NextQuestionYes.NextQuestionNo.Type.Should().Be(QuestionTypeEnum.Food);
            menuService.InitialQuestion.NextQuestionYes.NextQuestionNo.Description.Should().Be("Bolo de chocolate");
            menuService.InitialQuestion.NextQuestionYes.NextQuestionNo.PreviousQuestion.Should().Be(menuService.InitialQuestion.NextQuestionYes);
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Adjective);
            question.Description.Should().Be("Massa");
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Yes_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Food);
            question.Description.Should().Be("Lasanha");
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Yes_Yes_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.Food);
            question.Description.Should().Be("Bolo de chocolate");
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_Yes_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);

            //Assert
            question.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_No_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.AskUser);
            question.Description.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_Yes_No_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);

            //Assert
            question.Type.Should().Be(QuestionTypeEnum.AskUser);
            question.Description.Should().BeNull();
        }

        [Fact]
        public void Menu_NextQuestion_Answers_Yes_No_No_TypeAnswer_Yes_No_No_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);
            var question = menuService.InitialQuestion;

            //Act
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);

            question = menuService.AddQuestion(question, "Sorvete", "Gelado");

            question = menuService.GetNextQuestion(question, AnswerTypeEnum.Yes);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);
            question = menuService.GetNextQuestion(question, AnswerTypeEnum.No);

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
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);

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
            var previewDescription = menuService.GetPreviousFoodDescription(question3);

            //Assert
            previewDescription.Should().Be("Goiabada");
        }

        [Fact]
        public void Menu_GetPreviousFoodDescription_LastFood_Test()
        {
            //Arrange
            var questionService = new QuestionService();
            var menuService = new MenuService(questionService);

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
            var previewDescription = menuService.GetPreviousFoodDescription(question2);

            //Assert
            previewDescription.Should().Be("Churrasco");
        }
    }
}
