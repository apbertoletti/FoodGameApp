using FG.Domain.Enums;
using FG.Domain.Services;
using FluentAssertions;
using Xunit;

namespace FG.Tests.Services
{
    public class QuestionServiceTest
    {
        [Fact]
        public void GetInitialQuestion_Test()
        {
            //Arrange
            var questionService = new QuestionService();

            //Act
            var initialQuestion = questionService.GetInitialQuestion();

            //Assert
            initialQuestion.Should().NotBeNull();
            initialQuestion.Type.Should().Be(QuestionTypeEnum.Initial);

            initialQuestion.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Adjective);
            initialQuestion.NextQuestionYes.Description.Should().Be("Massa");
            initialQuestion.NextQuestionNo.Should().BeNull();

            initialQuestion.NextQuestionYes.NextQuestionYes.Type.Should().Be(QuestionTypeEnum.Food);
            initialQuestion.NextQuestionYes.NextQuestionYes.Description.Should().Be("Lasanha");
            initialQuestion.NextQuestionYes.NextQuestionYes.PreviousQuestion.Should().Be(initialQuestion.NextQuestionYes);

            initialQuestion.NextQuestionYes.NextQuestionNo.Type.Should().Be(QuestionTypeEnum.Food);
            initialQuestion.NextQuestionYes.NextQuestionNo.Description.Should().Be("Bolo de chocolate");
            initialQuestion.NextQuestionYes.NextQuestionNo.PreviousQuestion.Should().Be(initialQuestion.NextQuestionYes);
        }
    }
}
