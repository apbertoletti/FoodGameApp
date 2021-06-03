using FG.Domain;
using System;
using System.Collections.Generic;

namespace FG.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Pense num prato que voce gosta!");
            Console.WriteLine("(pressione ENTER após pensar)");
            Console.ReadKey();

            var menu = new Menu();
            var actualQuestion = menu.FirstQuestion;

            while (true)
            {
                switch (actualQuestion.Type)
                {
                    case QuestionTypeEnum.Adjective:
                        {
                            Console.Clear();
                            Console.WriteLine($"O prato que você pensou é {actualQuestion.Description}?");
                            Console.Write("Digite 'S' ou 'N': ");
                            var tecla = Console.ReadKey();
                            
                            actualQuestion = menu.NextQuestion(actualQuestion, (AnswerTypeEnum)tecla.Key);

                            break;
                        }

                    case QuestionTypeEnum.Food:
                        {
                            Console.Clear();
                            Console.WriteLine($"O prato que você pensou é {actualQuestion.Description}?");
                            Console.Write("Digite 'S' ou 'N': ");
                            var tecla = Console.ReadKey();
                            
                            actualQuestion = menu.NextQuestion(actualQuestion, (AnswerTypeEnum)tecla.Key);

                            if (actualQuestion is null)
                            {
                                Console.Clear();
                                Console.WriteLine("Acertei de novo!");
                                Console.ReadKey();
                            }

                            break;
                        }
                }    
            }

            //if (question.Food is null)
            //{
            //    Console.WriteLine($"O prato que você pensou é {question.Adjective}?");
            //    Console.Write("Digite 'S' ou 'N': ");
            //    var tecla = Console.ReadKey();
                
            //    var nextQuestion = menu.Answer(question, (AnswerTypeEnum)tecla.Key.ToString(), )
            //    if (tecla.Key == ConsoleKey.S)
            //    {
            //        Console.Clear();
            //        Console.WriteLine("o seu )
            //    }

            //}
        }
    }
}
