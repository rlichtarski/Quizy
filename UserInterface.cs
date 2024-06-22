namespace quizy
{
    public static class UserInterface
    {
        private static int loggedInUserId = -1;

        public static void DisplayMainMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Rejestracja\n2. Logowanie\n3. Rozpocznij quiz\n4. Historia quizów\n5. Wyjdź");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Register();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        if (loggedInUserId > -1)
                        {
                            StartQuiz();
                        }
                        else
                        {
                            Console.WriteLine("Musisz się zalogować.");
                        }
                        break;
                    case "4":
                        if (loggedInUserId > -1)
                        {
                            DisplayHistory();
                        }
                        else
                        {
                            Console.WriteLine("Musisz się zalogować.");
                        }
                        break;
                    case "5":
                    case "q":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór.");
                        break;
                }
            }
        }

        private static void Register()
        {
            Console.Write("Podaj email: ");
            var email = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            var password = Console.ReadLine();
            if (DatabaseManager.RegisterUser(email, password))
            {
                Console.WriteLine("Zarejestrowano!");
            }
            else
            {
                Console.WriteLine("Nie udało się zarejestrować. Email moze być juz zajęty.");
            }
        }

        private static void Login()
        {
            Console.Write("Podaj email: ");
            var email = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            var password = Console.ReadLine();
            loggedInUserId = DatabaseManager.LoginUser(email, password);
            if (loggedInUserId > -1)
            {
                Console.WriteLine("Zalogowano!");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy email lub hasło.");
            }
        }

        private static void StartQuiz()
        {
            Console.WriteLine("Wybierz kwalifikację: 1. EE.08 2. EE.09");
            Console.Write("Twój wybór: ");
            var categoryChoice = Console.ReadLine();
            string qualificationType = categoryChoice == "1" ? "ee08" : "ee09";
            Console.WriteLine($"Wybrana kwalifikacja: {qualificationType}");
            var questions = QuizManager.LoadQuestions(qualificationType, 15);

            QuizManager.RunQuiz(questions, loggedInUserId);
        }

        private static void DisplayHistory()
        {
            Console.WriteLine("Historie quizów będą dostępne wkrótce");
        }
    }
}