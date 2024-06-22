using Newtonsoft.Json;

namespace quizy
{
    public static class QuizManager
    {

        public static List<QuestionEntity> LoadQuestions(string category, int questionCount)
        {
            var questionsPath = $"{category}.json";
            var questionsJson = File.ReadAllText(questionsPath);
            var questions = JsonConvert.DeserializeObject<List<QuestionEntity>>(questionsJson);
            var random = new Random();
            return questions.OrderBy(q => random.Next()).Take(questionCount).ToList();
        }

        public static void RunQuiz(List<QuestionEntity> questions, int userId)
        {
            int correctAnswers = 0;
            int wrongAnswers = 0;

            foreach (var question in questions)
            {
                Console.WriteLine(question.Question);
                for (int i = 0; i < question.Answers.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {question.Answers[i]}");
                }
                Console.Write("Twój wybór: ");
                var userAnswer = Console.ReadLine();
                if (userAnswer != null || userAnswer != "" )
                {
                    if (int.Parse(userAnswer) >= 1 && int.Parse(userAnswer) <= 4)
                    {
                        if (userAnswer == "q") {
                            UserInterface.DisplayMainMenu();
                            return;
                        }
                        if (question.Answers[int.Parse(userAnswer) - 1] == question.Correct_Answer)
                        {
                            correctAnswers++;
                        }
                        else
                        {
                            wrongAnswers++;
                            Console.WriteLine($"Prawidłowa odpowiedź to: {question.Correct_Answer}");
                        }
                    } else {
                        Console.WriteLine("Nie ma takiej odpowiedzi!");
                    }
                }
            }
            Console.WriteLine($"Quiz zakończony! Poprawne odpowiedzi: {correctAnswers}, Błędne odpowiedzi: {wrongAnswers}");
            DatabaseManager.SaveQuizResult(userId, correctAnswers, wrongAnswers);
        }
    }
}
