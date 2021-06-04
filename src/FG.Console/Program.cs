using FG.Domain.Enums;
using FG.Domain.Services;
using System;

namespace FG.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            var actualQuestion = menu.InitialQuestion;

            while (true)
            {               
                switch (actualQuestion.Type)
                {
                    case QuestionTypeEnum.Initial:
                        {
                            Console.Clear();
                            Console.WriteLine("Pense num prato que voce gosta!");
                            Console.WriteLine("(pressione ENTER após pensar)");
                            Console.ReadKey();
                            
                            actualQuestion = menu.NextQuestion(actualQuestion, AnswerTypeEnum.Yes);
                            
                            break;
                        }

                    case QuestionTypeEnum.Adjective:
                        {
                            Console.Clear();
                            Console.WriteLine($"O prato que você pensou é {actualQuestion.Description}?");
                            Console.WriteLine("Digite 'S' ou 'N': ");
                            var answerType = GetAnswer();

                            actualQuestion = menu.NextQuestion(actualQuestion, answerType);

                            break;
                        }

                    case QuestionTypeEnum.Food:
                        {
                            Console.Clear();
                            Console.WriteLine($"O prato que você pensou é {actualQuestion.Description}?");
                            Console.WriteLine("Digite 'S' ou 'N': ");
                            var answerType = GetAnswer();

                            actualQuestion = menu.NextQuestion(actualQuestion, answerType);

                            if (actualQuestion is null)
                            {
                                Console.Clear();
                                Console.WriteLine("Acertei de novo!");
                                Console.ReadKey();
                                actualQuestion = menu.InitialQuestion;
                            }

                            break;
                        }

                    case QuestionTypeEnum.AskUser:
                        {
                            Console.Clear();
                            Console.WriteLine($"Qual é o prato que você pensou?");
                            Console.WriteLine("(digite e tecle ENTER)");
                            var foodNameChoosed = Console.ReadLine();

                            Console.Clear();
                            Console.WriteLine($"{foodNameChoosed} é ________ mas {menu.GetPreviousFoodDescription(actualQuestion)} não.");
                            Console.WriteLine("(digite e tecle ENTER)");
                            var foodAdjectiveChoosed = Console.ReadLine();

                            actualQuestion = menu.AddQuestion(actualQuestion, foodNameChoosed, foodAdjectiveChoosed);

                            break;
                        }
                }    
            }
        }

        private static AnswerTypeEnum GetAnswer()
        {
            while (true)
            {
                var keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.S)
                    return AnswerTypeEnum.Yes;

                if (keyPressed.Key == ConsoleKey.N)
                    return AnswerTypeEnum.No;
            }
        }
    }
}
