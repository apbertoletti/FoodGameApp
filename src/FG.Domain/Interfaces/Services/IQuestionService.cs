using FG.Domain.Entities;

namespace FG.Domain.Interfaces.Services
{
    public interface IQuestionService
    {
        Question GetInitialQuestion();
    }
}
